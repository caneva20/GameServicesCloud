namespace GameServicesCloud.Ranking;

public class LeaderboardPosition {
    public long UserId { get; init; }
    public double Score { get; init; }
    public int Position { get; init; }

    public LeaderboardPosition(long userId, double score, int position) {
        UserId = userId;
        Score = score;
        Position = position;
    }
}