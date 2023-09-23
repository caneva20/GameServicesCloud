using GameServicesCloud.Data;

namespace GameServicesCloud.Accounts;

public class UserToken : IEntity {
    public long Id { get; set; }
    public User User { get; set; } = null!;
    public string Token { get; set; } = null!;
}