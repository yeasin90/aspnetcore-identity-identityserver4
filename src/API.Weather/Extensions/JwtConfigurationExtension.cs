using Api.Weather.Configurations;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Api.Weather.Extensions
{
    public static class JwtConfigurationExtension
    {
        public static void ConfigureJwt(this IServiceCollection services)
        {
            var idsConfig = services.BuildServiceProvider().GetService<IOptions<IdentityServerConfiguration>>();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        // base-address of your identityserver
                        options.Authority = idsConfig.Value.IdentityServerHost;

                        // if you are using API resources, you can specify the name here
                        options.Audience = "app.api.weather";

                        // IdentityServer emits a typ header by default, recommended extra check
                        options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };

                        options.Events = new JwtBearerEvents()
                        {
                            OnAuthenticationFailed = context =>
                            {
                                return Task.CompletedTask;
                            },
                            OnTokenValidated = context =>
                            {
                                return Task.CompletedTask;
                            },
                            OnMessageReceived = context =>
                            {
                                return Task.CompletedTask;
                            },
                            OnChallenge = context =>
                            {
                                return Task.CompletedTask;
                            },
                            OnForbidden = context =>
                            {
                                return Task.CompletedTask;
                            },
                        };
                    });
        }
    }
}
