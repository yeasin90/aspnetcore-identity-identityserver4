using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthManagement.IdentityServer.Data.Seeds
{
    public static class SeedIdentityData
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            Log.Information("Started database seed");

            Log.Information("Started seeding user roles");
            await SeedRolesAsync(roleManager);
            Log.Information("Finished seeding user roles");


            Log.Information("Started seeding users");
            await SeedUsersAsync(userManager, "user1", "user1@user1.com", "123456", UserRoles.Admin, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, "User 1"),
                    new Claim(JwtClaimTypes.GivenName, "User1 GivenName"),
                    new Claim(JwtClaimTypes.FamilyName, "User1 FamilyName"),
                    new Claim(JwtClaimTypes.WebSite, "http://user1.com"),
                });
            await SeedUsersAsync(userManager, "user2", "user2@user2.com", "123456", UserRoles.Basic, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, "User 2"),
                    new Claim(JwtClaimTypes.GivenName, "User2 GivenName"),
                    new Claim(JwtClaimTypes.FamilyName, "User2 FamilyName"),
                    new Claim(JwtClaimTypes.WebSite, "http://user2.com"),
                    new Claim("location", "somewhere")
                });
            Log.Information("Finished seeding users");

            Log.Information("Finished seeding database");
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            try
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Moderator.ToString()));
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Basic.ToString()));
            }
            catch(Exception ex)
            {
                Log.Error(ex, $"Error on {nameof(SeedRolesAsync)}");
            }
        }

        private static async Task SeedUsersAsync(UserManager<IdentityUser> userManager, string username,
            string email, string password, UserRoles role, Claim[] claims)
        {
            try
            {
                var existingUser = await userManager.FindByNameAsync(username);

                if(existingUser != null)
                {
                    Log.Information($"User {username} already exist!");
                    return;
                }

                if (existingUser == null)
                {
                    var newUser = new IdentityUser()
                    {
                        UserName = username,
                        Email = email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true
                    };

                    await userManager.CreateAsync(newUser, password);
                    await userManager.AddClaimsAsync(newUser, claims);
                    await userManager.AddToRoleAsync(newUser, role.ToString());
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex, $"Error on {nameof(SeedUsersAsync)}");
            }
        }
    }
}
