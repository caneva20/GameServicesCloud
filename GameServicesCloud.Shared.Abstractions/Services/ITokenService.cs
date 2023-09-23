namespace GameServicesCloud;

public interface ITokenService {
    public const string Alphanumeric = "ABCDEFGHOJKLMNOPQRSTUVWXYZ0123456789";
    public const string Numeric = "0123456789";
    public const string Alphabet = "ABCDEFGHOJKLMNOPQRSTUVWXYZ";
    
    string? GenerateToken(int length, string characters = Numeric);
}