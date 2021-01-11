using AuthManagement.IdentityServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace AuthManagement.IdentityServer.Extensions
{
    public static class DbContextBuilderExtension
    {
        public static DbContextOptionsBuilder BuildSqlite(this DbContextOptionsBuilder builder, IServiceCollection services)
        {
            var config = services.BuildServiceProvider().GetService<IOptions<SqliteConfiguration>>();
            var connectionString = config.Value.ConnectionString;
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            return builder
                .UseSqlite(connectionString,
                    sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly));
        }

        // optional SqlServer support
        public static DbContextOptionsBuilder BuildSqlServer(this DbContextOptionsBuilder builder, IServiceCollection services)
        {
            var config = services.BuildServiceProvider().GetService<IOptions<SqlServerConfiguration>>();
            var connectionString = config.Value.ConnectionString;
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            return builder
                .UseSqlServer(connectionString,
                       sql => sql.MigrationsAssembly(migrationsAssembly));
        }
    }
}
