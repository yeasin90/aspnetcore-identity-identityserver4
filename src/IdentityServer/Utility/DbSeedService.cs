﻿using AuthManagement.IdentityServer.Configurations;
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
                foreach (var apiScope in IdentityServerConfigurations.ApiScopes)
                {
                    if(!ctx.ApiScopes.Any(x => x.Name == apiScope.Name))
                    {
                        await ctx.ApiScopes.AddAsync(apiScope.ToEntity());
                    }
                }

                foreach (var apiResource in IdentityServerConfigurations.ApisResources)
                {
                    if (!ctx.ApiResources.Any(x => x.Name == apiResource.Name))
                    {
                        await ctx.ApiResources.AddAsync(apiResource.ToEntity());
                    }
                }

                foreach (var client in IdentityServerConfigurations.Clients)
                {
                    if (!ctx.Clients.Any(x => x.ClientId == client.ClientId))
                    {
                        await ctx.Clients.AddAsync(client.ToEntity());
                    }
                }

                await ctx.SaveChangesAsync();
            }
        }

        private async Task SeedIdentityRoles()
        {
            Log.Information("Started seeding user roles");

            foreach (var applicationRole in IdentityConfigurations.ApplicationRoles)
            {
                await RoleManager.CreateAsync(applicationRole);
            }

            Log.Information("Finished seeding user roles");
        }

        private async Task SeedIdentityUsers()
        {
            Log.Information("Started seeding users");

            foreach (var applicationUser in IdentityConfigurations.ApplicationUsers)
            {
                await UserManager.CreateAsync(applicationUser, applicationUser.Password);
                await UserManager.AddClaimsAsync(applicationUser, applicationUser.Claims);
                await UserManager.AddToRoleAsync(applicationUser, applicationUser.Role.ToString());
            }

            Log.Information("Finished seeding users");
        }
    }
}
