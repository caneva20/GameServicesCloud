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

    private readonly EmailVerificationOptions _verificationOptions;

    public UserService(ILogger<UserService> logger,
        ITokenService tokenService,
        IOptions<EmailVerificationOptions> verificationOptions,
        IRepository<User> userRepository,
        IMailService mailService,
        IMailTemplateService mailTemplateService) {
        _logger = logger;
        _tokenService = tokenService;
        _userRepository = userRepository;
        _mailService = mailService;
        _mailTemplateService = mailTemplateService;
        _verificationOptions = verificationOptions.Value;
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

        user.HasVerifiedEmail = false;
        user.EmailVerificationCode = GenerateVerificationCode();

        var newUser = await _userRepository.Save(user);

        _logger.LogInformation("User {Email} registered with id {Id}", newUser.Email, newUser.Id);

        return newUser;
    }

    public string SendVerificationEmail(User user, Func<string, string, string> endpointBuilder) {
        var verificationEndpoint = endpointBuilder(user.Email, user.EmailVerificationCode);

        var mailBody = _mailTemplateService.Load(_verificationOptions.TemplateName,
            new Dictionary<string, string> {
                { "EMAIL", user.Email },
                { "VERIFICATION_LINK", verificationEndpoint }
            })!;

        _mailService.SendMail(user.Email, _verificationOptions.EmailSubject, mailBody);

        return verificationEndpoint;
    }

    private string GenerateVerificationCode() {
        return _tokenService.GenerateToken(_verificationOptions.TokenLength, _verificationOptions.TokenCharacters);
    }
}