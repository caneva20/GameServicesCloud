namespace GameServicesCloud.Accounts;

public class AccountActivationOptions {
    public string TokenCharacters { get; set; } = null!;
    public int TokenLength { get; set; }
    public string EmailSubject { get; set; } = null!;
    public string TemplateName { get; set; } = null!;
}