using GameServicesCloud.Data;

namespace GameServicesCloud.Ranking;

public class UserScore : IEntity {
    public long Id { get; set; }
    public long UserId { get; set; }
    public Leaderboard Leaderboard { get; set; } = null!;
    public double Score { get; set; }
}