namespace GameServicesCloud.Accounts;

public interface IAuthService {
    Task<bool> GenerateLoginToken(User user);
    Task<AuthToken?> GenerateAuthToken(User user, string loginToken);
}