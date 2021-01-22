using AuthManagement.IdentityServer.Extensions;
using AuthManagement.IdentityServer.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace AuthManagement.IdentityServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.InitializeConfigurations(Configuration);

            services.ConfigureSqLite();

            services.ConfigureIdentity();

            services.ConfigureIdentityServer();

            services.AddScoped<IDbMigrationService, DbMigrationService>();
            services.AddScoped<IDbSeedService, DbSeedService>();
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Apply migrations and initialize database with dummy data
            await InitializeDb(serviceProvider);

            app.UseRouting();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("IdentityServer is up and running!");
                });
            });
        }

        private async Task InitializeDb(IServiceProvider serviceProvider)
        {
            var migrationService = serviceProvider.GetService<IDbMigrationService>();
            var dbSeedService = serviceProvider.GetService<IDbSeedService>();

            await migrationService.ApplyMigration();
            await dbSeedService.SeedIdentityDb();
            await dbSeedService.SeedIdentityServerDb();
        }
    }
}
