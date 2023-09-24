namespace GameServicesCloud.Accounts;

public interface IUserService {
    Task<bool> IsRegistered(string email);
    Task<User?> RegisterUser(User user);
}