namespace GameServicesCloud.Accounts;

public interface IUserTokenService {
    Task<UserToken?> GenerateToken(User user);
}