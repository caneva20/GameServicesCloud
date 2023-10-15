using Microsoft.EntityFrameworkCore;

namespace GameServicesCloud.Persistence;

public class PersistenceDbContext : DbContext {
    public PersistenceDbContext(DbContextOptions<PersistenceDbContext> options) : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
    }
}