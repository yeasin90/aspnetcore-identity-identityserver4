using Api.Weather.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Weather.Extensions
{
    public static class InitializeConfigurations
    {
        public static void BindGlobalConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IdentityServerConfiguration>(configuration.GetSection(nameof(IdentityServerConfiguration)));
        }
    }
}
