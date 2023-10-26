using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace GameServicesCloud.Accounts;

public class JwtService : IJwtService {
    private readonly JwtOptions _options;
    private readonly IUserClaimService _claimService;

    public JwtService(IOptions<JwtOptions> options, IUserClaimService claimService) {
        _claimService = claimService;
        _options = options.Value;
    }

    public async Task<AuthToken> GenerateToken(User user) {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var expirationTime = DateTime.UtcNow.AddMinutes(_options.ExpirationTimeInMinutes);
        var token = new JwtSecurityToken(_options.Issuer,
            _options.Audience,
            GetClaims(user, await _claimService.GetDefaultClaims()).DistinctBy(x => x.Type),
            expires: expirationTime,
            signingCredentials: signingCredentials);

        return new AuthToken {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpirationTime = expirationTime
        };
    }

    private static IEnumerable<Claim> GetClaims(User user, IEnumerable<AccountClaim> defaultClaims) {
        yield return new Claim(ClaimTypes.NameIdentifier, user.Id.ToString());
        yield return new Claim(ClaimTypes.Name, user.Email);
        yield return new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());

        foreach (var claim in user.Claims.Select(ConvertClaim)) {
            yield return claim;
        }

        foreach (var claim in defaultClaims.Select(ConvertClaim)) {
            yield return claim;
        }
    }

    private static Claim ConvertClaim(AccountClaim x) {
        return new Claim(x.Name, "true");
    }
}