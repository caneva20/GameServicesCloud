namespace GameServicesCloud.Ranking;

public interface ILeaderboardService {
    Task<Leaderboard?> Find(string name);

    Task<bool> Create(string name);

    Task Delete(Leaderboard leaderboard);

    Task SaveScore(Leaderboard leaderboard, long userId, double score);

    Task<IEnumerable<LeaderboardPosition>> GetLeaderboard(Leaderboard leaderboard, int page = 0, int pageSize = 100);
}