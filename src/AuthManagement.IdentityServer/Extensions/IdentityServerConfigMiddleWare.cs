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
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer()
            // nuget for AddAspNetIdentity : IdentityServer4.AspNetIdentity
            .AddAspNetIdentity<IdentityUser>()
            // nuget for AddConfigurationStore : IdentityServer4.EntityFramework
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder => builder.UseSqlite(connectionString,
                    opt => opt.MigrationsAssembly(migrationsAssembly));
            })
            // nuget for AddConfigurationStore : IdentityServer4.EntityFramework
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder => builder.UseSqlite(connectionString,
                    opt => opt.MigrationsAssembly(migrationsAssembly));
            })
            .AddDeveloperSigningCredential();
        }
    }
}
