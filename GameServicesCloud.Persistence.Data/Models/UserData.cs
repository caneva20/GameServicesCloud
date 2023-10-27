using GameServicesCloud.Data;

namespace GameServicesCloud.Persistence;

public class UserData : IEntity {
    public long Id { get; set; }
    public long UserId { get; set; }
    public byte[] Data { get; set; } = null!;
    public DateTime? UpdatedAt { get; set; }
}