using AuthManagement.IdentityServer.Data.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AuthManagement.IdentityServer.Extensions
{
    public static class IdentityServerConfigMiddleWare
    {
        public static void ConfigureIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>() // nuget : IdentityServer4.AspNetIdentity
                .UseDatabaseStore(configuration) // Replace with UseInmemoryStore() to use in-memory version
                .AddDeveloperSigningCredential(); // don't use this in production. Check google how to read from a real certificate
        }

        private static IIdentityServerBuilder UseDatabaseStore(this IIdentityServerBuilder builder, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            return
                // nuget : IdentityServer4.EntityFramework
                builder.AddConfigurationStore(options =>
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
                .AddInMemoryIdentityResources(SeedIdentityServerData.IdentityResources)
                .AddInMemoryApiResources(SeedIdentityServerData.ApiResources)
                .AddInMemoryApiScopes(SeedIdentityServerData.ApiScopes);
                //.AddInMemoryClients(Config.Clients)
        }
    }
}
