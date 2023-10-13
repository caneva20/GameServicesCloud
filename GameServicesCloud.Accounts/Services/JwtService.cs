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

    public AuthToken GenerateToken(User user) {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim> {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email)
        };

        claims.AddRange(user.Claims.Select(x => new Claim(x.Name, "true")));

        var expirationTime = DateTime.UtcNow.AddMinutes(_options.ExpirationTimeInMinutes);
        var token = new JwtSecurityToken(_options.Issuer, _options.Audience, claims, expires: expirationTime, signingCredentials: signingCredentials);

        return new AuthToken {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpirationTime = expirationTime
        };
    }
}