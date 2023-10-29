using Microsoft.EntityFrameworkCore;

namespace GameServicesCloud.Ranking;

public class RankingDbContext : DbContext {
    public RankingDbContext(DbContextOptions<RankingDbContext> options) : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
    }
}