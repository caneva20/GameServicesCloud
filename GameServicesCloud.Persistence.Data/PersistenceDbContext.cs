using Microsoft.EntityFrameworkCore;

namespace GameServicesCloud.Persistence;

public class PersistenceDbContext : DbContext {
    public DbSet<UserData> UserDatas { get; set; } = null!;

    public PersistenceDbContext(DbContextOptions<PersistenceDbContext> options) : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
    }
}