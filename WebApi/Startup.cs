using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #region Consul注册
            //站点启动完成 执行且只执行一次
            Configuration.ConsulRegister();

            #endregion
        }
    }

    public static class ConsulHelper
    {
        public static void ConsulRegister(this IConfiguration configuration)
        {
            ConsulClient client=new ConsulClient(c =>
            {
                c.Address=new Uri("http://localhost:8500");
                c.Datacenter = "dc1";
            });

            string ip = configuration["ip"];
            int port = int.Parse(configuration["port"]);

            client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = $"Server{ip}:{port}",
                Name = "DamonApiMicroService",
                Address = ip,
                Port = port,
                Check = new AgentServiceCheck()
                {
                    Interval = TimeSpan.FromSeconds(10),
                    HTTP = $"http://{ip}:{port}/api/health/index",
                    Timeout = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(20)
                }
            });
        }
    }
}
