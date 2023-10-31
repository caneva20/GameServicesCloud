using Refit;

namespace GameServicesCloud.UI.Clients.Ranking;

public interface ILeaderboardApi {
    [Get("/admin/leaderboard")]
    Task<IEnumerable<Leaderboard>> FindAll([Query] int page, [Query] int pageSize, [Query] string? filter);

    [Get("/admin/leaderboard/count")]
    Task<int> Count([Query] string? filter);

    [Post("/admin/leaderboard")]
    Task Create(CrateLeaderboardRequest request);

    [Delete("/admin/leaderboard")]
    Task Delete([Query] string name);

    [Get("/leaderboard/{name}/count")]
    Task<int> GetLeaderboardCount(string name);

    [Get("/leaderboard/{name}")]
    Task<IEnumerable<LeaderboardPosition>> GetLeaderboard(string name, [Query] int page, [Query] int pageSize);
}