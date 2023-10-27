using GameServicesCloud.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameServicesCloud.Persistence;

public static class PersistenceDataExtensions {
    public static void AddData(this IServiceCollection services, string connectionString) {
        services.AddDbContext<DbContext, PersistenceDbContext>(options => {
            options.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(PersistenceDataExtensions).Assembly.FullName));
        });

        services.AddScoped<IRepository<UserData>, UserDataRepository>();
    }
}