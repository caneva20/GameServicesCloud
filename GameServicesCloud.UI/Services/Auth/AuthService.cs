using GameServicesCloud.UI.HttpsClients;

namespace GameServicesCloud.UI.Services.Auth;

public interface IAuthService {
    Task Login(LoginRequest request);
    LoginResult? CurrentLogin { get; }
    Task Logout();
    Task SendToken(string email);
}

public class AuthService : IAuthService {
    private readonly AccountsHttpClient _http;

    public LoginResult? CurrentLogin { get; private set; }

    public AuthService(AccountsHttpClient http) {
        _http = http;
    }

    public async Task SendToken(string email) {
        if (!await _http.StartLogin(email)) {
            throw new Exception("Failed to generate new token");
        }
    }

    public async Task Login(LoginRequest request) {
        CurrentLogin = await _http.FinishLogin(request);
    }

    public Task Logout() {
        CurrentLogin = null;

        return Task.CompletedTask;
    }
}

public class LoginRequest {
    public string Email { get; set; }
    public string LoginToken { get; set; }
}

public record LoginResult(string Token, DateTime ExpirationTime);