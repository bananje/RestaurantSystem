﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using LuckyFoodSystem.Application.Common.Constants;

namespace LuckyFoodSystem.Identity.Infrastructure.Configuration;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope(Scopes.LuckyFoodSystemWeb),
            new ApiScope(Scopes.LuckyFoodSystemApi),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "internal.client",
                ClientName = "Internal Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { Scopes.LuckyFoodSystemWeb, Scopes.LuckyFoodSystemApi }
            },

            new Client
            {
                ClientId = "interactive.client",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:7055/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:7055/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:7055/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    Scopes.LuckyFoodSystemApi
                }
            },
        };
}
