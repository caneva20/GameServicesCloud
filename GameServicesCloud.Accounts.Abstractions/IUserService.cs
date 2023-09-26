namespace GameServicesCloud.Accounts;

public interface IUserService {
    Task<bool> IsRegistered(string email);
    Task<User?> RegisterUser(User user);
    string SendActivationEmail(User user, Func<string, string, string> endpointBuilder);
}