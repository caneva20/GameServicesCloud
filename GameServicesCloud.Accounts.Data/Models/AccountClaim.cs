using GameServicesCloud.Data;

namespace GameServicesCloud.Accounts;

public class AccountClaim : IEntity {
    public long Id { get; set; }
    public bool IsDefault { get; set; }
    public string Name { get; set; } = null!;
}