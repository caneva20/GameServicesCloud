namespace GameServicesCloud.Accounts;

public interface IUserTokenService {
    Task<UserToken?> GenerateToken(User user);
    Task<UserToken?> Find(User user, string token);
    Task Delete(UserToken token);
}