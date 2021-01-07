using System.Collections.Generic;
using IdentityServer4.Models;

namespace ClientCredentialsIdentityServer
{
    public static class Config
    {

        public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>()
        {
            new ApiScope("api1","My API1"),
            new ApiScope("api2","My API2")
        };

        public static IEnumerable<Client> Clients=>
        new List<Client>()
        {
            new Client()
            {
                ClientId = "client_id",
                // secret for authentication
                ClientSecrets = {new Secret("secret".Sha256())},
                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // scopes that client has access to 
                AllowedScopes = {"api1","api2"}
            }
        };
    }
}