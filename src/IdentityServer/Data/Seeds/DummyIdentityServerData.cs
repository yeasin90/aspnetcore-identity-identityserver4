using IdentityServer4.Models;
using System.Collections.Generic;

namespace AuthManagement.IdentityServer.Data.Seeds
{
    public static class DummyIdentityServerData
    {
        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource 
                {
                    Name = "app.api.whatever",
                    DisplayName = "Whatever Apis",
                    ApiSecrets = { new Secret("a75a559d-1dab-4c65-9bc0-f8e590cb388d".Sha256()) },
                    Scopes = new List<string> { "app.api.whatever.read", "app.api.whatever.write", "app.api.whatever.full" }
                },
                new ApiResource()
                {
                    Name = "app.api.weather",
                    DisplayName = "Weather Apis",
                    ApiSecrets = { new Secret("E38171FD-C40A-40F1-9CBE-63C5F6D60AE8".Sha256()) },
                    Scopes = new List<string> { "app.api.weather.read", "app.api.weather.write" }
                }
            };

        public static List<Client> Clients => new List<Client>
            {
                new Client
                    {
                         ClientName = "Client for Whatever",
                         ClientId = "t8agr5xKt4$3",
                         AllowedGrantTypes = GrantTypes.ClientCredentials,
                         ClientSecrets = { new Secret("eb300de4-add9-42f4-a3ac-abd3c60f1919".Sha256()) },
                         AllowedScopes = new List<string> { "app.api.whatever.read", "app.api.whatever.write" }
                    },
                    new Client
                    {
                         ClientName = "Client for Weather",
                         ClientId = "3X=nNv?Sgu$S",
                         AllowedGrantTypes = GrantTypes.ClientCredentials,
                         ClientSecrets = { new Secret("1554db43-3015-47a8-a748-55bd76b6af48".Sha256()) },
                         AllowedScopes = new List<string> { "app.api.weather.read" } // this client is only allowed to read Weather data
                    }
            };
    }
}
