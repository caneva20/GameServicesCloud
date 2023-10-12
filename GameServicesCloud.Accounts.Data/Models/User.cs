using GameServicesCloud.Data;

namespace GameServicesCloud.Accounts;

public class User : IEntity {
    public long Id { get; set; }
    public string Email { get; set; } = null!;
    public bool IsActivated { get; set; }
    public DateTime? ActivatedAt { get; set; }
    public string ActivationCode { get; set; } = null!;

    public ICollection<AccountClaim> Claims { get; set; } = new List<AccountClaim>();
}