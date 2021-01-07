using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InteractiveAspNetCoreMvcClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            }).AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    //options.Authority = "https://localhost:5001";

                    options.Authority = "http://localhost:5001";
                    options.RequireHttpsMetadata = false;


                    options.ClientId = "mvc";
                    options.ClientSecret = "secret";

                    options.ResponseType = "code";

                    options.SaveTokens = true;
                });


            /*   http  安全问题
             *由于最新版的Chrome的Cookie策略导致写Cookie失败,从而导致用户认证的失败.
SameSite=strict:对于来自不同于源站的站点发出的请求，不发送cookie,为了防止CSRF攻击。
SameSite=lax:类似于strict，但是当用户有意地通过单击链接或发送表单启动请求时，就会发送cookies。不会在脚本请求时发送。
SameSite=none:无论请求来自哪里都可以(但是需要https)。
             *
             */
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCookiePolicy();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //The RequireAuthorization method disables anonymous access for the entire application
                endpoints.MapDefaultControllerRoute()
                    .RequireAuthorization();
            });
        }
    }
}
