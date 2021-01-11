using AuthManagement.IdentityServer.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthManagement.IdentityServer.Data.Seeds
{
    public static class SeedIdentityData
    {
        public static async Task SeedAsync(IServiceScope scope)
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await SeedRolesAsync(roleManager);
            await SeedUsersAsync(userManager);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            Log.Information("Started seeding user roles");

            foreach (var applicationRole in ApplicationRoles)
            {
                await roleManager.CreateAsync(applicationRole);
            }

            Log.Information("Finished seeding user roles");
        }

        private static async Task SeedUsersAsync(UserManager<IdentityUser> userManager)
        {
            Log.Information("Started seeding users");

            foreach (var applicationUser in ApplicationUsers)
            {
                await userManager.CreateAsync(applicationUser, applicationUser.Password);
                await userManager.AddClaimsAsync(applicationUser, applicationUser.Claims);
                await userManager.AddToRoleAsync(applicationUser, applicationUser.Role.ToString());
            }

            Log.Information("Finished seeding users");
        }

        public static List<ApplicationRole> ApplicationRoles => new List<ApplicationRole>
        {
            new ApplicationRole(RoleTypes.SuperAdmin),
            new ApplicationRole(RoleTypes.Admin),
            new ApplicationRole(RoleTypes.Moderator),
            new ApplicationRole(RoleTypes.Basic)
        };

        public static List<ApplicationUser> ApplicationUsers => new List<ApplicationUser>
        {
            new ApplicationUser()
            {
                UserName = "user1",
                Email = "user1@user1.com",
                Password = "123456",
                Role = new ApplicationRole(RoleTypes.Admin),
                Claims = new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, "User 1"),
                    new Claim(JwtClaimTypes.GivenName, "User1 GivenName"),
                    new Claim(JwtClaimTypes.FamilyName, "User1 FamilyName"),
                    new Claim(JwtClaimTypes.WebSite, "http://user1.com"),
                }
            },
            new ApplicationUser()
            {
                UserName = "user2",
                Email = "user2@user2.com",
                Password = "123456",
                Role = new ApplicationRole(RoleTypes.Basic),
                Claims = new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, "User 2"),
                    new Claim(JwtClaimTypes.GivenName, "User2 GivenName"),
                    new Claim(JwtClaimTypes.FamilyName, "User2 FamilyName"),
                    new Claim(JwtClaimTypes.WebSite, "http://user2.com"),
                }
            }
        };
    }
}
