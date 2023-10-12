namespace GameServicesCloud.Accounts;

public class UserDto {
    public string Email { get; set; } = null!;
    public bool HasVerifiedEmail { get; set; }

    public ICollection<AccountClaimDto> Claims { get; set; } = new List<AccountClaimDto>();
}