namespace GameServicesCloud.Accounts;

public class UserTokenOptions {
    public int MaxRetries { get; set; }
    public int TokenLength { get; set; }
    public int ExpirationTimeInSeconds { get; set; }
    public string TokenCharacters { get; set; } = null!;
}