using GameServicesCloud.Data;

namespace GameServicesCloud.Accounts;

public class User : IEntity {
    public long Id { get; set; }
    public string Email { get; set; } = null!;
    public bool HasVerifiedEmail { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public string EmailVerificationCode { get; set; } = null!;
}