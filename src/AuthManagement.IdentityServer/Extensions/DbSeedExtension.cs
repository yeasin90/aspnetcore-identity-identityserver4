using AuthManagement.IdentityServer.Data.Seeds;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Threading.Tasks;

namespace AuthManagement.IdentityServer.Extensions
{
    /// <summary>
    /// Seed Identity and IdentityServer database
    /// </summary>
    /// <param name="host"></param>
    /// <returns></returns>
    public static class DbSeedExtension
    {
        public static async Task SeedDatabase(this IHost host)
        {
            Log.Information("Start database seed");
            using (var scope = host.Services.CreateScope())
            {
                await SeedIdentityData.SeedAsync(scope);
                await SeedIdentityServerData.SeedAsync(scope);
            }
            Log.Information("Finished database seed");
        }
    }
}
