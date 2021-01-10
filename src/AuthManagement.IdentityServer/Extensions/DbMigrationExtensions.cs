using AuthManagement.IdentityServer.Data;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AuthManagement.IdentityServer.Extensions
{
    /// <summary>
    /// Apply database migrations for Identity and IdentityServer
    /// IdentityServer internally has two db context
    /// check : migration-scripts.txt file for migration commands
    /// </summary>
    /// <param name="host"></param>
    public static class DbMigrationExtensions
    {
        public static void ApplyDbMigration(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                Log.Information("Applying migrations");
                // For Identity
                ApplyMigration<ApplicationDbContext>(scope);
                // For IdentityServer's internal PersistedGrantDbContext
                ApplyMigration<PersistedGrantDbContext>(scope);
                // For IdentityServer's internal ConfigurationDbContext
                ApplyMigration<ConfigurationDbContext>(scope);
                Log.Information("Finished migration apply");
            }
        }

        private static void ApplyMigration<T>(IServiceScope scope) where T : DbContext
        {
            var services = scope.ServiceProvider;
            var context = services.GetService<T>();
            context.Database.Migrate();
        }
    }
}
