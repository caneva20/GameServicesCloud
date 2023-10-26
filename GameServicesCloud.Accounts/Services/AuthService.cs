using GameServicesCloud.Mail;
using Microsoft.Extensions.Options;

namespace GameServicesCloud.Accounts;

public class AuthService : IAuthService {
    private readonly AuthOptions _options;

    private readonly IUserTokenService _userTokenService;
    private readonly IMailService _mailService;
    private readonly IMailTemplateService _mailTemplateService;
    private readonly IJwtService _jwtService;

    public AuthService(
        IUserTokenService userTokenService,
        IMailService mailService,
        IMailTemplateService mailTemplateService,
        IOptions<AuthOptions> options,
        IJwtService jwtService) {
        _userTokenService = userTokenService;
        _mailService = mailService;
        _mailTemplateService = mailTemplateService;
        _jwtService = jwtService;
        _options = options.Value;
    }

    public async Task<bool> GenerateLoginToken(User user) {
        var token = await _userTokenService.GenerateToken(user);

        if (token == null) {
            return false;
        }

        SendEmail(user, token);

        return true;
    }

    public async Task<AuthToken?> GenerateAuthToken(User user, string loginToken) {
        var userToken = await _userTokenService.Find(user, loginToken);

        if (userToken == null) {
            return null;
        }

        await _userTokenService.Delete(userToken);

        return await _jwtService.GenerateToken(user);
    }

    private void SendEmail(User user, UserToken token) {
        var mailBody = _mailTemplateService.Load(_options.NewTokenTemplateName,
            new Dictionary<string, string> {
                { "USER", user.Email },
                { "TOKEN", token.Token }
            })!;

        _mailService.SendMail(user.Email, _options.EmailTitle, mailBody);
    }
}