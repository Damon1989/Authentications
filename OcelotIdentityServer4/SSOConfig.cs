using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace OcelotIdentityServer4
{
    public class SSOConfig
    {
        public static IEnumerable<ApiResource> GetApiResources(IConfigurationSection section)
        {
            var resource=new List<ApiResource>();
            if (section != null)
            {
                var configs=new List<ApiConfig>();
                section.Bind("ApiResources",configs);
                foreach (var config in configs)
                {
                    resource.Add(new ApiResource(config.Name,config.DisplayName));
                }
            }

            return resource.ToArray();
        }

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>()
            {
                new ApiScope("api1","My API1"),
                new ApiScope("api2","My API2"),
            };

        public static IEnumerable<Client> GetClients(IConfigurationSection section)
        {
            var clients=new List<Client>();
            if (section != null)
            {
                var configs=new List<ClientConfig>();
                section.Bind("Clients",configs);
                foreach (var config in configs)
                {
                    var client = new Client {ClientId = config.ClientId};
                    var clientSecrets=new List<Secret>();
                    foreach (var secret in config.ClientSecrets)
                    {
                        clientSecrets.Add(new Secret(secret.Sha256()));
                    }

                    client.ClientSecrets = clientSecrets.ToArray();

                    var grantTypes=new GrantTypes();
                    var allowedGrantTypes = grantTypes.GetType().GetProperty(config.AllowedGrantTypes);
                    client.AllowedGrantTypes = allowedGrantTypes == null
                        ? GrantTypes.ClientCredentials
                        : (ICollection<string>) allowedGrantTypes.GetValue(grantTypes, null);

                    client.AllowedScopes = config.AllowedScopes.ToArray();

                    Console.WriteLine($"{string.Join(",",client.AllowedScopes.ToArray())}");

                    clients.Add(client);
                }
            }

            return clients.ToArray();
        }
    }

    public class ApiConfig
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }

    }

    public class ClientConfig
    {
        public string ClientId { get; set; }
        public IList<string> ClientSecrets { get; set; }
        public string AllowedGrantTypes { get; set; }
        public List<string> AllowedScopes { get; set; }
    }
}
