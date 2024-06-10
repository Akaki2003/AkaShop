using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace AkaShop.Infrastructure.Data
{
    public static class DbContextExtensions
    {
        public static IServiceCollection AddAkaShopDb(this IServiceCollection services, IConfiguration configuration)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var connString = configuration.GetConnectionString("DefaultConnection");
            return services.AddDbContext<ApplicationDbContext>((DbContextOptionsBuilder opt) =>
            {
                opt.UseNpgsql(connString, (NpgsqlDbContextOptionsBuilder a) =>
                {
                    a.MigrationsAssembly("AkaShop.Infrastructure");
                });
            });
        }
    }
}
