using AuthManagement.IdentityServer.Data.Seeds;
using Microsoft.AspNetCore.Identity;
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
            using (var scope = host.Services.CreateScope())
            {
                await SeedIdentityDatabase(scope);
                await SeedIdentityServerDatabase(scope);
            }
        }

        private static async Task SeedIdentityDatabase(IServiceScope scope)
        {
            var services = scope.ServiceProvider;
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Log.Information("Started Identity database seed");
            await SeedIdentityData.SeedAsync(userManager, roleManager);
            Log.Information("Finished Identity seeding database");
        }

        private static async Task SeedIdentityServerDatabase(IServiceScope scope)
        {
            
        }
    }
}
