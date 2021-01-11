using AuthManagement.IdentityServer.Data;
using AuthManagement.IdentityServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace AuthManagement.IdentityServer.Extensions
{
    public static class SqLiteConfigMiddleWare
    {
        public static void ConfigureSqLite(this IServiceCollection services)
        {
            var sqliteConfiguration = services.BuildServiceProvider().GetService<IOptions<SqliteConfiguration>>();
            var connectionString = sqliteConfiguration.Value.ConnectionStrings;
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString,
                    sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));
        }
    }
}
