using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;

namespace IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<ApiResource> GetApis() =>
            new List<ApiResource>
            {
                new ApiResource("ApiOne")
            };
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("ApiOne","My API")
            };

        public static IEnumerable<Client> GetClients()=>
        new List<Client>
        {
            new Client()
            {
                ClientId = "client_id",
                ClientSecrets = {new Secret("client_secret".ToSha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "ApiOne" }
            }
        };
    }
}
