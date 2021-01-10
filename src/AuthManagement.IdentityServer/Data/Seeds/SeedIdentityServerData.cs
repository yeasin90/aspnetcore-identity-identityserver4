using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthManagement.IdentityServer.Data.Seeds
{
    public static class SeedIdentityServerData
    {
        public static async Task SeedAsync(IServiceScope scope)
        {
            using (var ctx = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>())
            {
                if (!ctx.ApiScopes.Any())
                {
                    foreach (var apiScope in ApiScopes)
                    {
                        await ctx.ApiScopes.AddAsync(apiScope.ToEntity());
                    }
                }

                if (!ctx.ApiResources.Any())
                {
                    foreach (var apiResource in ApiResources)
                    {
                        await ctx.ApiResources.AddAsync(apiResource.ToEntity());
                    }
                }

                if (!ctx.IdentityResources.Any())
                {
                    foreach (var identityResource in IdentityResources)
                    {
                        await ctx.IdentityResources.AddAsync(identityResource.ToEntity());
                    }
                }

                await ctx.SaveChangesAsync();
            }
        }

        private static IEnumerable<ApiScope> ApiScopes => new[]
        {
            new ApiScope("weatherapi.read"),
            new ApiScope("weatherapi.write"),
        };

        private static IEnumerable<ApiResource> ApiResources => new[]
        {
            new ApiResource("weatherapi")
            {
                Scopes = new List<string> { "weatherapi.read", "weatherapi.write" },
                ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
                UserClaims = new List<string> {"role"}
            }
        };

        private static IEnumerable<IdentityResource> IdentityResources => new[]
        {
            new IdentityResources.OpenId(),
            // Don't know below. Need to google
            //new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string> {"role"}
            }
        };
    }
}
