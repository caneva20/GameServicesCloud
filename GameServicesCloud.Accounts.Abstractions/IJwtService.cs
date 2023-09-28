namespace GameServicesCloud.Accounts;

public interface IJwtService {
    AuthToken GenerateToken(long userId, string userEmail);
}