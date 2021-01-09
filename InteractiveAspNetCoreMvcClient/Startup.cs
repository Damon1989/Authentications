using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.ObjectPool;

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
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                {
                    //options.Authority = "https://localhost:5001";

                    options.Authority = "http://localhost:5001";
                    options.RequireHttpsMetadata = false;


                    options.ClientId = "mvc";
                    options.ClientSecret = "secret";

                    options.ResponseType = "code";

                    //access Token �洢��Cookie��
                    options.SaveTokens = true;
                    
                    
                    options.Scope.Clear();
                    options.Scope.Add(OidcConstants.StandardScopes.OpenId);
                    options.Scope.Add(OidcConstants.StandardScopes.Profile);
                    options.Scope.Add(OidcConstants.StandardScopes.Email);
                    options.Scope.Add(OidcConstants.StandardScopes.Address);
                    options.Scope.Add("damon");

                    options.Scope.Add(OidcConstants.StandardScopes.OfflineAccess);
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
