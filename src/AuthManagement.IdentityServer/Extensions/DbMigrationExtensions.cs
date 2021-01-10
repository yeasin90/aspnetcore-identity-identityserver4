using AuthManagement.IdentityServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AuthManagement.IdentityServer.Extensions
{
    public static class DbMigrationExtensions
    {
        public static void ApplyMigration<T>(this IHost host) where T : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<T>();
                context.Database.Migrate();
            }
        }
    }
}
