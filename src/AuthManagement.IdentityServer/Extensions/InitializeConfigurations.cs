using AuthManagement.IdentityServer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthManagement.IdentityServer.Extensions
{
    public static class ConfigurationExtensions
    {
        // nuget : Microsoft.Extensions.Options
        // Bind appSettings.json value into concrete object
        // Objects are injectable
        public static void InitializeConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SqliteConfiguration>(options => configuration.GetSection(nameof(SqliteConfiguration)).Bind(options));
        }
    }
}
