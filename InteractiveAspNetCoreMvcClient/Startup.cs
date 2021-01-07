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


            /*   http  ��ȫ����
             *�������°��Chrome��Cookie���Ե���дCookieʧ��,�Ӷ������û���֤��ʧ��.
SameSite=strict:�������Բ�ͬ��Դվ��վ�㷢�������󣬲�����cookie,Ϊ�˷�ֹCSRF������
SameSite=lax:������strict�����ǵ��û������ͨ���������ӻ��ͱ���������ʱ���ͻᷢ��cookies�������ڽű�����ʱ���͡�
SameSite=none:���������������ﶼ����(������Ҫhttps)��
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
