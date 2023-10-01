using System.Net;
using System.Net.Http.Json;
using GameServicesCloud.UI.Services.Auth;

namespace GameServicesCloud.UI.HttpsClients;

public class AccountsHttpClient {
    private readonly HttpClient _http;

    public AccountsHttpClient(HttpClient http) {
        _http = http;
    }

    public async Task<bool> StartLogin(string email) {
        var response = await _http.PostAsync($"Auth?email={email}", null);

        return response.IsSuccessStatusCode;
    }

    public async Task<LoginResult?> FinishLogin(LoginRequest request) {
        var response = await _http.PostAsync("Auth/Token", JsonContent.Create(request));

        if (response.StatusCode == HttpStatusCode.BadRequest) {
            throw new Exception("Invalid credentials");
        }

        if (!response.IsSuccessStatusCode) {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<LoginResult>();
    }
}