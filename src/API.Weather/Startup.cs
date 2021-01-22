using Api.Weather.Extensions;
using Api.Weather.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Api.Weather
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
            services.ConfigureApiVersioning();

            services.InitializeGlobalConfigurations(Configuration);

            services.ConfigureJwt();

            services.RegisterServices();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // await CheckIdentityServer(serviceProvider) did not worked
            // it caused app.UseRouting(); not to execute and api was not running
            CheckIdentityServer(serviceProvider).Wait();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Api server is up and running!");
                });
            });
        }

        private async Task CheckIdentityServer(IServiceProvider serviceProvider)
        {
            var authService = serviceProvider.GetService<IAuthenticationService>();
            await authService.PingIdentityServerRunning();
        }
    }
}
