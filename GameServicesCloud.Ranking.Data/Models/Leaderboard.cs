using GameServicesCloud.Data;

namespace GameServicesCloud.Ranking;

public class Leaderboard : IEntity {
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<UserScore> Scores { get; set; } = new List<UserScore>();
}