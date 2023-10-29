namespace GameServicesCloud.Ranking;

public interface ILeaderboardService {
    Task<Leaderboard?> Find(string name);

    Task<bool> Create(string name);

    Task Delete(Leaderboard leaderboard);
}