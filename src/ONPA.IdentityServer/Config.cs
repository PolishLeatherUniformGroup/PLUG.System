using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace ONPA.IdentityServer;


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
            new ApiScope("apply-api", "Apply API"),
            new ApiScope("gatherings-api", "Gatherings API"),
            new ApiScope("membership-api", "Membership API"),
            new ApiScope("organizations-api", "Organizations API"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "onpa-webapp",
                AllowedGrantTypes = GrantTypes.Code,
                ClientSecrets =
                {
                    new Secret("onpasecret".Sha256())
                },
                RedirectUris = { "https://localhost:5002/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "apply-api", 
                    "gatherings-api",
                    "membership-api",
                    "organizations-api"
                }
            }
        };
}