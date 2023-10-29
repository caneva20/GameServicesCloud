namespace GameServicesCloud.UI;

public record Leaderboard(string Name, int Scores);

public record CrateLeaderboardRequest(string Name);