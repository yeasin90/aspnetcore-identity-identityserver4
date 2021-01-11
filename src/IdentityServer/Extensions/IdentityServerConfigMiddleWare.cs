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
                .UseDatabaseStore(services) 
                .AddDeveloperSigningCredential(); // don't use this in production. Check google how to read from a real certificate
        }

        private static IIdentityServerBuilder UseDatabaseStore(this IIdentityServerBuilder builder, IServiceCollection services)
        {
            return builder
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.BuildSqlite(services);
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.BuildSqlite(services);
                });
        }
    }
}
