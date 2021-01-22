﻿using IdentityServer4.Models;
using System.Collections.Generic;

namespace AuthManagement.IdentityServer.Configurations
{
    public static class IdentityServerConfigurations
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("app.api.hotels.read"),
                new ApiScope("app.api.hotels.write"),
                new ApiScope("app.api.hotels.full"),
                new ApiScope("app.api.weather.read"),
                new ApiScope("app.api.weather.write")
            };

        public static IEnumerable<ApiResource> ApisResources =>
            new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "app.api.hotels",
                    DisplayName = "Api Hotels",
                    ApiSecrets = { new Secret("a75a559d-1dab-4c65-9bc0-f8e590cb388d".Sha256()) },
                    Scopes = new List<string> { "app.api.hotels.read", "app.api.hotels.write", "app.api.hotels.full" }
                },
                new ApiResource()
                {
                    Name = "app.api.weather",
                    DisplayName = "Api Weather",
                    ApiSecrets = { new Secret("E38171FD-C40A-40F1-9CBE-63C5F6D60AE8".Sha256()) },
                    Scopes = new List<string> { "app.api.weather.read", "app.api.weather.write" }
                }
            };

        public static List<Client> Clients => new List<Client>
            {
                new Client
                    {
                         ClientName = "Client for Hotels Api",
                         ClientId = "t8agr5xKt4$3",
                         ClientSecrets = { new Secret("eb300de4-add9-42f4-a3ac-abd3c60f1919".Sha256()) },
                         AllowedGrantTypes = GrantTypes.ClientCredentials,
                         AllowedScopes = new List<string> { "app.api.hotels.read", "app.api.hotels.write" },
                    },
                    new Client
                    {
                         ClientName = "Client for Weather Api",
                         ClientId = "3X=nNv?Sgu$S",
                         ClientSecrets = { new Secret("1554db43-3015-47a8-a748-55bd76b6af48".Sha256()) },
                         AllowedGrantTypes = GrantTypes.ClientCredentials,
                         AllowedScopes = new List<string> { "app.api.weather.read" }, 
                    }
            };
    }
}