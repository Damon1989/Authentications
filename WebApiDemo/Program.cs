using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        /// <summary>
        /// ConfigureWebHostDefaults
        /// ConfigureHostConfiguration
        /// ConfigureAppConfiguration
        /// ConfigureServices
        /// ConfigureLogging
        /// Startup
        /// Startup.ConfigureServices
        /// Startup.Configure
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder =>
                {
                    Console.WriteLine("ConfigureAppConfiguration");
                })
                .ConfigureServices(service =>
                {
                    Console.WriteLine("ConfigureService");
                })
                .ConfigureHostConfiguration(builder =>
                {
                    Console.WriteLine("ConfigureHostConfiguration");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    Console.WriteLine("ConfigureWebHostDefaults");
                    webBuilder.UseStartup<Startup>();

                    //webBuilder.ConfigureServices(services =>
                    //{
                    //    Console.WriteLine("webBuilder.ConfigureServices");
                    //    services.AddControllers();
                    //});

                    //webBuilder.Configure(app =>
                    //{
                    //    Console.WriteLine("webBuilder.Configure");
                    //    //if (env.IsDevelopment())
                    //    //{
                    //    //    app.UseDeveloperExceptionPage();
                    //    //}

                    //    app.UseRouting();

                    //    app.UseAuthorization();

                    //    app.UseEndpoints(endpoints =>
                    //    {
                    //        endpoints.MapControllers();
                    //    });
                    //});
                });
    }
}
