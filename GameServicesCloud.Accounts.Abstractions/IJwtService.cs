namespace GameServicesCloud.Accounts;

public interface IJwtService {
    Task<AuthToken> GenerateToken(User user);
}