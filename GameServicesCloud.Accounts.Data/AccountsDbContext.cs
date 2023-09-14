using Microsoft.EntityFrameworkCore;

namespace GameServicesCloud.Accounts;

public class AccountsDbContext : DbContext {
    public DbSet<User> Users { get; set; } = null!;

    public AccountsDbContext(DbContextOptions<AccountsDbContext> options) : base(options) {
    }
}