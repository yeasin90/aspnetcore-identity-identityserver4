using AuthManagement.IdentityServer.Data;
using Microsoft.Extensions.DependencyInjection;

namespace AuthManagement.IdentityServer.Extensions
{
    public static class SqLiteConfigMiddleWare
    {
        public static void ConfigureSqLite(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.BuildSqlite(services));
        }
    }
}
