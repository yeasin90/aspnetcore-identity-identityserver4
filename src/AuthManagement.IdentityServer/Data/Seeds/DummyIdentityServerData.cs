using IdentityServer4.Models;
using System.Collections.Generic;

namespace AuthManagement.IdentityServer.Data.Seeds
{
    public static class DummyIdentityServerData
    {
        public static List<ApiScope> ApiScopes => new List<ApiScope>
        {
            new ApiScope("weatherapi.read"),
            new ApiScope("weatherapi.write"),
        };

        public static List<ApiResource> ApiResources => new List<ApiResource>
        {
            new ApiResource("weatherapi")
            {
                Scopes = new List<string> { "weatherapi.read", "weatherapi.write" },
                ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
                UserClaims = new List<string> {"role"}
            }
        };

        public static List<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            // Don't know below. Need to google
            //new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string> {"role"}
            }
        };

        public static List<Client> Clients => new List<Client>
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "scope1" }
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:44300/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "scope2" }
                },
                new Client
                {
                    ClientId = "testclient",
                    ClientSecrets = new [] { new Secret("testSecret".Sha512()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = { "weather" }
                },
            };
    }
}
