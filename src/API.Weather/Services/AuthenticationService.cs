using Api.Weather.Configurations;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Weather.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IOptions<IdentityServerConfiguration> _identityServerConfiguration;
        private DiscoveryDocumentResponse _discoveryDocumentResponse;
        public AuthenticationService(IOptions<IdentityServerConfiguration> identityServerConfiguration)
        {
            _identityServerConfiguration = identityServerConfiguration;
        }

        public async Task<string> GetToken(string scope)
        {
            TokenResponse response = new TokenResponse();
            using (var client = new HttpClient())
            {
                response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = _discoveryDocumentResponse.TokenEndpoint,
                    ClientId = _identityServerConfiguration.Value.ClientId,
                    ClientSecret = _identityServerConfiguration.Value.ClientSecret,
                    Scope = scope
                });
            }

            if (response.IsError)
                throw new Exception("Unable to get token", response.Exception);

            return response.AccessToken;
        }

        public async Task PingIdentityServerRunning()
        {
            using (var client = new HttpClient())
            {
                _discoveryDocumentResponse = await client.GetDiscoveryDocumentAsync(_identityServerConfiguration.Value.IdentityServerHost);
            }

            if (_discoveryDocumentResponse.IsError)
                throw new Exception("Failed to ping IdentityServer", _discoveryDocumentResponse.Exception);
        }
    }
}
