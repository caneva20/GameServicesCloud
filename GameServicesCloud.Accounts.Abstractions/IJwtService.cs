namespace GameServicesCloud.Accounts;

public interface IJwtService {
    AuthToken GenerateToken(User user);
}