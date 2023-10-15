namespace GameServicesCloud.Accounts;

public class AccountClaimDto {
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsDefault { get; set; }
}

public class CreateAccountClaimDto {
    public string Name { get; set; } = null!;
}