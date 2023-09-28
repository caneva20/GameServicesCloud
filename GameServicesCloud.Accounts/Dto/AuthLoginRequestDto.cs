namespace GameServicesCloud.Accounts;

public class AuthLoginRequestDto {
    public string Email { get; init; } = null!;
    public string LoginToken { get; init; } = null!;
}