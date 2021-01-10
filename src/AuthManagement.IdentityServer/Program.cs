using AuthManagement.IdentityServer.Data;
using AuthManagement.IdentityServer.Data.Seeds;
using AuthManagement.IdentityServer.Extensions;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System.Threading.Tasks;

namespace AuthManagement.IdentityServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ConfigureLogger();
            var host = CreateHostBuilder(args).Build();
            ApplyMigrations(host);
            await SeedDatabase(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        /// <summary>
        /// Configure Serilog logger
        /// </summary>
        private static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
                .CreateLogger();
        }

        /// <summary>
        /// Seed Identity database
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        private async static Task SeedDatabase(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await SeedIdentityData.SeedAsync(userManager, roleManager);
            }
        }

        /// <summary>
        /// Apply database migrations for Identity and IdentityServer
        /// IdentityServer internally has two db context
        /// check : migration-scripts.txt file for migration commands
        /// </summary>
        /// <param name="host"></param>
        private static void ApplyMigrations(IHost host)
        {
            Log.Information("Applying migrations");
            // For Identity
            host.ApplyMigration<ApplicationDbContext>();
            // For IdentityServer's internal PersistedGrantDbContext
            host.ApplyMigration<PersistedGrantDbContext>();
            // For IdentityServer's internal ConfigurationDbContext
            host.ApplyMigration<ConfigurationDbContext>();
            Log.Information("Finished migration apply");
        }
    }
}
