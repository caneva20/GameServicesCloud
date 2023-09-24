namespace GameServicesCloud.Accounts;

public class EmailVerificationOptions {
    public string TokenCharacters { get; set; } = null!;
    public int TokenLength { get; set; }
}