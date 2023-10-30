using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;
using RestaurantMenu.Utils;

namespace IdentityServerAspNetIdentity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()         
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("Menu", "Menu")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {                    
            // interactive client using code flow + pkce
            new Client
            {
                ClientId = WC.ClientsName.MenuWebApp.ToString(),
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:7235/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:7235/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes =
                { 
                    "Menu",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    JwtClaimTypes.Role
                },
                AlwaysIncludeUserClaimsInIdToken = true,
            },
        };
}
