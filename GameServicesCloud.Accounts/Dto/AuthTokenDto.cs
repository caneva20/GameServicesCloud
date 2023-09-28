namespace GameServicesCloud.Accounts;

public class AuthTokenDto {
    public string Token { get; set; } = null!;
    public DateTime ExpirationTime { get; set; }
}