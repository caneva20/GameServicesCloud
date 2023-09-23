namespace GameServicesCloud.Accounts;

public interface IUserTokenService {
    Task<string?> GenerateToken(User user);
}