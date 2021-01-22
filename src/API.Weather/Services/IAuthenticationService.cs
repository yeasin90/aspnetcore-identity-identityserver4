using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Weather.Services
{
    public interface IAuthenticationService
    {
        Task<string> GetToken(string scope);
        Task PingIdentityServerRunning();
    }
}
