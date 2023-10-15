using System.Net;
using Blazored.LocalStorage;
using GameServicesCloud.UI.Clients;
using Refit;

namespace GameServicesCloud.UI.Services.Auth;

public interface IAuthService {
    Task Login(LoginRequest request);
    Task<LoginResult?> CurrentLogin { get; }
    Task Logout();
    Task SendToken(string email);
}

public class AuthService : IAuthService {
    private readonly IAuthApi _authApi;
    private readonly ILocalStorageService _localStorage;

    private LoginResult? _currentLogin;
    private bool _initialized;

    public Task<LoginResult?> CurrentLogin => GetCurrentToken();

    public AuthService(IAuthApi authApi, ILocalStorageService localStorage) {
        _authApi = authApi;
        _localStorage = localStorage;
    }

    public async Task SendToken(string email) {
        try {
            await _authApi.StartLogin(email);
        } catch (ApiException e) {
            throw new Exception(e.ReasonPhrase);
        }
    }

    public async Task Login(LoginRequest request) {
        try {
            _currentLogin = await _authApi.FinishLogin(request);

            await SaveTokenToStorage(_currentLogin);
        } catch (ApiException e) {
            if (e.StatusCode == HttpStatusCode.BadRequest) {
                throw new Exception("Invalid token");
            }

            throw new Exception(e.ReasonPhrase);
        }
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