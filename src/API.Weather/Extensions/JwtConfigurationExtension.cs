using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Api.Weather.Extensions
{
    public static class JwtConfigurationExtension
    {
        public static void ConfigureJwt(this IServiceCollection services)
        {
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        // base-address of your identityserver
                        options.Authority = "https://localhost:5001";

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
