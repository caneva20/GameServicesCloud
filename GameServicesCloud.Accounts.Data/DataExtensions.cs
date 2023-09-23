using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameServicesCloud.Accounts;

public static class AccountDataExtensions {
    public static void AddData(this IServiceCollection services, string connectionString) {
        services.AddDbContext<DbContext, AccountsDbContext>(options => {
            options.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(AccountDataExtensions).Assembly.FullName));
        });
    }
}