using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace GameServicesCloud.UI.Services.Auth;

public class AuthStateProvider : AuthenticationStateProvider {
    private readonly IAuthService _authService;

    public AuthStateProvider(IAuthService authService) {
        _authService = authService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
        if (_authService.CurrentLogin == null) {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(_authService.CurrentLogin!.Token);

        var claimsIdentity = new ClaimsIdentity(jwtSecurityToken.Claims, "GameServicesCloud authentication");

        return new AuthenticationState(new ClaimsPrincipal(claimsIdentity));
    }

    public async Task GetToken(string email) {
        await _authService.SendToken(email);
    }

    public async Task Login(LoginRequest request) {
        await _authService.Login(request);

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task Logout() {
        await _authService.Logout();

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}