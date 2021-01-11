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
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace OcelotServer
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
            //var identityServerConfig=new IdentityServerConfig();
            //Configuration.Bind("IdentityServerConfig",identityServerConfig);
            //services.AddAuthentication(identityServerConfig.IdentityScheme)
            //    .AddJwtBearer(identityServerConfig.IdentityScheme, options =>
            //    {
            //        options.RequireHttpsMetadata = false;
            //        options.Authority = $"http://{identityServerConfig.ServerIP}:{identityServerConfig.ServerPort}";
            //        options.TokenValidationParameters = new TokenValidationParameters()
            //        {
            //            ValidateAudience = false
            //        };
            //    });
            //    //.AddIdentityServerAuthentication(options =>
            //    //{
            //    //    options.RequireHttpsMetadata = false;
            //    //    options.Authority = $"http://{identityServerConfig.ServerIP}:{identityServerConfig.ServerPort}";
            //    //    options.ApiName = identityServerConfig.ScopeName;
            //    //});

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("ApiScope", policy =>
            //    {
            //        //policy.RequireAuthenticatedUser();
            //        policy.RequireClaim("scope", "api1");
            //    });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            //app.UseAuthentication();
            app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public class IdentityServerConfig
    {
        public string ServerIP { get; set; }
        public string ServerPort { get; set; }
        public string IdentityScheme { get; set; }
        public string ScopeName { get; set; }
    }
}
