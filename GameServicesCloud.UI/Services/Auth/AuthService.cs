using Blazored.LocalStorage;
using GameServicesCloud.UI.Clients;

namespace GameServicesCloud.UI.Services.Auth;

public interface IAuthService {
    Task Login(LoginRequest request);
    Task<LoginResult?> CurrentLogin { get; }
    Task Logout();
    Task SendToken(string email);
}

public class AuthService : IAuthService {
    private readonly AccountsHttpClient _http;
    private readonly ILocalStorageService _localStorage;

    private LoginResult? _currentLogin;
    private bool _initialized;

    public Task<LoginResult?> CurrentLogin => GetCurrentToken();

    public AuthService(AccountsHttpClient http, ILocalStorageService localStorage) {
        _http = http;
        _localStorage = localStorage;
    }

    public async Task SendToken(string email) {
        if (!await _http.StartLogin(email)) {
            throw new Exception("Failed to generate new token");
        }
    }

    public async Task Login(LoginRequest request) {
        _currentLogin = await _http.FinishLogin(request);

        await SaveTokenToStorage(_currentLogin);
    }

    public async Task Logout() {
        _currentLogin = null;

        await SaveTokenToStorage(_currentLogin);
    }

    private async Task<LoginResult?> GetCurrentToken() {
        if (!_initialized) {
            _currentLogin = await LoadTokenFromStorage();

            _initialized = true;
        }

        return _currentLogin;
    }

    private ValueTask<LoginResult?> LoadTokenFromStorage() {
        return _localStorage.GetItemAsync<LoginResult?>("auth_token");
    }

    private async Task SaveTokenToStorage(LoginResult? result) {
        await _localStorage.SetItemAsync("auth_token", result);
    }
}