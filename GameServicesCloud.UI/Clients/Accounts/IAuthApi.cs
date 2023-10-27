using Refit;

namespace GameServicesCloud.UI.Clients.Accounts;

public interface IAuthApi {
    [Post("/auth")]
    Task StartLogin([Query] string email);

    [Post("/auth/token")]
    Task<LoginResult?> FinishLogin(LoginRequest request);
}