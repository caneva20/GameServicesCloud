using GameServicesCloud.Data;
using GameServicesCloud.Mail;
using Microsoft.Extensions.Options;

namespace GameServicesCloud.Accounts;

public class UserService : IUserService {
    private readonly ILogger<UserService> _logger;
    private readonly ITokenService _tokenService;
    private readonly IRepository<User> _userRepository;
    private readonly IMailService _mailService;
    private readonly IMailTemplateService _mailTemplateService;

    private readonly AccountActivationOptions _activationOptions;

    public UserService(ILogger<UserService> logger,
        ITokenService tokenService,
        IOptions<AccountActivationOptions> verificationOptions,
        IRepository<User> userRepository,
        IMailService mailService,
        IMailTemplateService mailTemplateService) {
        _logger = logger;
        _tokenService = tokenService;
        _userRepository = userRepository;
        _mailService = mailService;
        _mailTemplateService = mailTemplateService;
        _activationOptions = verificationOptions.Value;
    }

    public Task<bool> IsRegistered(string email) {
        return _userRepository.Exists(x => x.Email == email);
    }

    public async Task<User?> RegisterUser(User user) {
        _logger.LogInformation("Registering user {Email}", user.Email);

        if (await IsRegistered(user.Email)) {
            _logger.LogInformation("User registration failed. Email already registered");
            return null;
        }

        user.IsActivated = false;
        user.ActivationCode = GenerateActivationCode();

        var newUser = await _userRepository.Save(user);

        _logger.LogInformation("User {Email} registered with id {Id}", newUser.Email, newUser.Id);

        return newUser;
    }

    public string SendActivationEmail(User user, Func<string, string, string> endpointBuilder) {
        var activationEndpoint = endpointBuilder(user.Email, user.ActivationCode);

        var mailBody = _mailTemplateService.Load(_activationOptions.TemplateName,
            new Dictionary<string, string> {
                { "EMAIL", user.Email },
                { "ACTIVATION_LINK", activationEndpoint }
            })!;

        _mailService.SendMail(user.Email, _activationOptions.EmailSubject, mailBody);

        return activationEndpoint;
    }

    private string GenerateActivationCode() {
        return _tokenService.GenerateToken(_activationOptions.TokenLength, _activationOptions.TokenCharacters);
    }
}