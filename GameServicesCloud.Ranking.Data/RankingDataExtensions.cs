using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameServicesCloud.Ranking;

public static class RankingDataExtensions {
    public static void AddData(this IServiceCollection services, string connectionString) {
        services.AddDbContext<DbContext, RankingDbContext>(options => {
            options.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(RankingDataExtensions).Assembly.FullName));
        });
    }
}