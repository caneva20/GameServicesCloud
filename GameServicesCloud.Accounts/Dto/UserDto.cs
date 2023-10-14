namespace GameServicesCloud.Accounts;

public class UserDto {
    public long Id { get; set; }
    public string Email { get; set; } = null!;

    public ICollection<AccountClaimDto> Claims { get; set; } = new List<AccountClaimDto>();
}

public class CreateUserDto {
    public string Email { get; set; } = null!;
}