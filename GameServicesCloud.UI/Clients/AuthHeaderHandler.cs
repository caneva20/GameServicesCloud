using System.Net.Http.Headers;
using GameServicesCloud.UI.Services.Auth;

namespace GameServicesCloud.UI.Clients;

public class AuthHeaderHandler : DelegatingHandler {
    private readonly IAuthService _authService;

    public AuthHeaderHandler(IAuthService authService) {
        _authService = authService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
        var token = (await _authService.CurrentLogin)?.Token;

        if (token != null) {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}