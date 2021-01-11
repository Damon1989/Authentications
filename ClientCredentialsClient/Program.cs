using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace ClientCredentialsClient
{
    /// <summary>
    /// dotnet ClientCredentialsClient.dll --urls="http://*:7001"
    /// </summary>
    class Program
    {

        /// <summary>
        ///  获取token  postman 需要把content-type修改为 application/x-www-form-urlencoded  参考地址
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {

            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:9010");

            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            //request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = disco.TokenEndpoint,
                ClientId = "damon",
                ClientSecret = "damonSecrets",
                //Scope = "api1 api2"
                Scope = "api1"
            });

            Console.WriteLine($"tokenResponse.IsError:{tokenResponse.IsError}");
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }
            Console.WriteLine(tokenResponse.Json);


            //call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("http://localhost:9002/api/ocelot/aggrdamon");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"response.StatusCode:{response.StatusCode}");
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }

            Console.Read();



            //// discover endpoints from metadata
            //var client=new HttpClient();
            //var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5001");

            //if (disco.IsError)
            //{
            //    Console.WriteLine(disco.Error);
            //    return;
            //}

            ////request token
            //var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            //{
            //    Address = disco.TokenEndpoint,
            //    ClientId = "client_id",
            //    ClientSecret = "secret",
            //    //Scope = "api1 api2"
            //    Scope = "api1"
            //    //Scope = ""
            //});

            //Console.WriteLine($"tokenResponse.IsError:{tokenResponse.IsError}");
            //if (tokenResponse.IsError)
            //{
            //    Console.WriteLine(tokenResponse.Error);
            //    return;
            //}
            //Console.WriteLine(tokenResponse.Json);

            ////call api
            //var apiClient=new HttpClient();
            //apiClient.SetBearerToken(tokenResponse.AccessToken);

            //var response = await apiClient.GetAsync("http://localhost:6001/identity/info");
            //if (!response.IsSuccessStatusCode)
            //{
            //    Console.WriteLine($"response.StatusCode:{response.StatusCode}");
            //}
            //else
            //{
            //    var content = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine(JArray.Parse(content));
            //}


            Console.Read();
        }
    }
}
