using AuthManagement.IdentityServer.Data.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

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
            return builder
                // nuget : IdentityServer4.EntityFramework
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.BuildSqlite(services);
                })
                // nuget : IdentityServer4.EntityFramework
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.BuildSqlite(services);
                });
        }

        private static IIdentityServerBuilder UseInmemoryStore(this IIdentityServerBuilder builder)
        {
            return builder
                .AddInMemoryApiResources(DummyIdentityServerData.Apis)
                .AddInMemoryClients(DummyIdentityServerData.Clients);
        }
    }
}
