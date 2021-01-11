using AuthManagement.IdentityServer.Data.Seeds;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System.Linq;
using System.Threading.Tasks;

namespace AuthManagement.IdentityServer.Utility
{
    public class DbSeedService : IDbSeedService
    {
        private readonly ConfigurationDbContext configurationDbContext;

        public DbSeedService(UserManager<IdentityUser> userManager
            , RoleManager<IdentityRole> roleManager
            , ConfigurationDbContext configurationDbContext)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            this.configurationDbContext = configurationDbContext;
        }

        public UserManager<IdentityUser> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }

        public async Task SeedIdentityDb()
        {
            await SeedIdentityRoles();
            await SeedIdentityUsers();
        }

        public async Task SeedIdentityServerDb()
        {
            using (var ctx = configurationDbContext)
            {
                if (!ctx.ApiScopes.Any())
                {
                    foreach (var apiScope in DummyIdentityServerData.ApiScopes)
                    {
                        await ctx.ApiScopes.AddAsync(apiScope.ToEntity());
                    }
                }

                if (!ctx.ApiResources.Any())
                {
                    foreach (var apiResource in DummyIdentityServerData.ApiResources)
                    {
                        await ctx.ApiResources.AddAsync(apiResource.ToEntity());
                    }
                }

                if (!ctx.IdentityResources.Any())
                {
                    foreach (var identityResource in DummyIdentityServerData.IdentityResources)
                    {
                        await ctx.IdentityResources.AddAsync(identityResource.ToEntity());
                    }
                }

                await ctx.SaveChangesAsync();
            }
        }

        private async Task SeedIdentityRoles()
        {
            Log.Information("Started seeding user roles");

            foreach (var applicationRole in DummyIdentityData.ApplicationRoles)
            {
                await RoleManager.CreateAsync(applicationRole);
            }

            Log.Information("Finished seeding user roles");
        }

        private async Task SeedIdentityUsers()
        {
            Log.Information("Started seeding users");

            foreach (var applicationUser in DummyIdentityData.ApplicationUsers)
            {
                await UserManager.CreateAsync(applicationUser, applicationUser.Password);
                await UserManager.AddClaimsAsync(applicationUser, applicationUser.Claims);
                await UserManager.AddToRoleAsync(applicationUser, applicationUser.Role.ToString());
            }

            Log.Information("Finished seeding users");
        }
    }
}
