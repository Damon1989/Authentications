using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Consul;

namespace WebApi.Controllers
{

    /*
     * nginx 监听请求--转发请求-- 响应
     * 单纯是负载均衡
     * 好处： 客户端轻松 --其他全部 Nginx
     * 缺陷： 做不到自动发现   --  自动移除
     *
     */

    /*
     * consul.exe agent --dev
    */


    /// <summary>
    /// dotnet WebApi.dll --urls= "http://*:7001"
    /// dotnet WebApi.dll --urls= "http://*:7002"
    /// dotnet WebApi.dll --urls= "http://*:7003"
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet,Route("Index")]
        public IEnumerable<User> Index()
        {
            var result = new List<User>
            {
                new User()
                {
                    Id = "1",
                    Name = "xx",
                    IP = Request.HttpContext.Connection.LocalIpAddress.ToString(),
                    Port = Request.HttpContext.Connection.LocalPort.ToString()
                }
            };
            return result;
        }

        [HttpGet,Route("nginxinfo")]
        public string GetInfo()
        {
            var url = "http://localhost:8080/home/index";
            return InvokeApi(url);
        }

        [HttpGet,Route("consulinfo")]
        public string GetConsulInfo()
        {
            var url = "http://DamonApiMicroService/home/index";
            ConsulClient client=new ConsulClient(c =>
            {
                c.Address=new Uri("http://localhost:8500");
                c.Datacenter = "dc1";
            });
            var response = client.Agent.Services().Result.Response;
            foreach (var item in response)
            {
                Console.WriteLine("****************");
                Console.WriteLine(item.Key);
                var service = item.Value;
                Console.WriteLine($"{service.Address}--{service.Port}--{service.Service}");

                Console.WriteLine("******************");
            }

            var uri=new Uri(url);
            string groupName = uri.Host;
            AgentService agentService = null;

            var serviceDictionary = response
                .Where(c => c.Value.Service.Equals(groupName, StringComparison.OrdinalIgnoreCase)).ToArray();
            agentService = serviceDictionary[0].Value;
            url = $"{uri.Scheme}://{agentService.Address}:{agentService.Port}{uri.PathAndQuery}";
            return InvokeApi(url);
        }


        public static string InvokeApi(string url)
        {
            using var httpClient=new HttpClient();
            var message = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri =new Uri(url) 
            };
            var result = httpClient.SendAsync(message).Result;
            return result.IsSuccessStatusCode ? 
                result.Content.ReadAsStringAsync().Result : 
                $"error :{result.EnsureSuccessStatusCode()}";
        }
    }

    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public string Port { get; set; }
    }
}
