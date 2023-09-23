using System.Text;

namespace GameServicesCloud;

public class TokenService : ITokenService {
    public string GenerateToken(int length, string characters = ITokenService.Numeric) {
        var sb = new StringBuilder();

        for (var i = 0; i < length; i++) {
            var randomChar = characters[Random.Shared.Next(characters.Length)];
            sb.Append(randomChar);
        }

        return sb.ToString();
    }
}