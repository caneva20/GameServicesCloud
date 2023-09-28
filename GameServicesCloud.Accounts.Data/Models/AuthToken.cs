namespace GameServicesCloud.Accounts;

public class AuthToken {
    public string Token { get; set; } = null!;
    public DateTime ExpirationTime { get; set; }
}