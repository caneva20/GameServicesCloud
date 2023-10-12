using Microsoft.EntityFrameworkCore;

namespace GameServicesCloud.Accounts;

public class AccountsDbContext : DbContext {
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserToken> UserTokens { get; set; } = null!;

    public AccountsDbContext(DbContextOptions<AccountsDbContext> options) : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<User>().HasMany(x => x.Claims).WithMany();
    }
}