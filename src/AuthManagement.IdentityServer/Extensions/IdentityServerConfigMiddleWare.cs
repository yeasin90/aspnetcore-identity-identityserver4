using AuthManagement.IdentityServer.Data.Seeds;
using AuthManagement.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace AuthManagement.IdentityServer.Extensions
{
    public static class IdentityServerConfigMiddleWare
    {
        public static void ConfigureIdentityServer(this IServiceCollection services)
        {
            services
                .AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>() // nuget : IdentityServer4.AspNetIdentity
                .UseDatabaseStore(services) // Replace with UseInmemoryStore() to use in-memory version
                .AddDeveloperSigningCredential(); // don't use this in production. Check google how to read from a real certificate
        }

        private static IIdentityServerBuilder UseDatabaseStore(this IIdentityServerBuilder builder, IServiceCollection services)
        {
            var sqliteConfiguration = services.BuildServiceProvider().GetService<IOptions<SqliteConfiguration>>();
            var connectionString = sqliteConfiguration.Value.ConnectionStrings;
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            return builder
                // nuget : IdentityServer4.EntityFramework
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseSqlite(connectionString,
                        opt => opt.MigrationsAssembly(migrationsAssembly));
                })
                // nuget : IdentityServer4.EntityFramework
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseSqlite(connectionString,
                        opt => opt.MigrationsAssembly(migrationsAssembly));
                });
        }

        private static IIdentityServerBuilder UseInmemoryStore(this IIdentityServerBuilder builder)
        {
            return builder
                .AddInMemoryIdentityResources(DummyIdentityServerData.IdentityResources)
                .AddInMemoryApiResources(DummyIdentityServerData.ApiResources)
                .AddInMemoryApiScopes(DummyIdentityServerData.ApiScopes)
                .AddInMemoryClients(DummyIdentityServerData.Clients);
        }
    }
}
