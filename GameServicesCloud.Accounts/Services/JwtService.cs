using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace GameServicesCloud.Accounts;

public class JwtService : IJwtService {
    private readonly JwtOptions _options;

    public JwtService(IOptions<JwtOptions> options) {
        _options = options.Value;
    }

    public AuthToken GenerateToken(long userId, string userEmail) {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim> {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Email, userEmail)
        };

        var expirationTime = DateTime.UtcNow.AddMinutes(_options.ExpirationTimeInMinutes);
        var token = new JwtSecurityToken(_options.Issuer, _options.Audience, claims, expires: expirationTime, signingCredentials: signingCredentials);

        return new AuthToken {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpirationTime = expirationTime
        };
    }
}