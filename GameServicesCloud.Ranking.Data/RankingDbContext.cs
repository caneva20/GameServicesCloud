using Microsoft.EntityFrameworkCore;

namespace GameServicesCloud.Ranking;

public class RankingDbContext : DbContext {
    public DbSet<Leaderboard> Leaderboards { get; set; }

    public RankingDbContext(DbContextOptions<RankingDbContext> options) : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
    }
}