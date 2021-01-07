using IdentityServer4;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InteractiveAspNetCoreIdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddTestUsers(TestUsers.Users);
            builder.AddDeveloperSigningCredential();

            //services.AddAuthentication()
            //    .AddGoogle("Google", options =>
            //    {
            //        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

            //        options.ClientId = "<insert here>";
            //        options.ClientSecret = "<insert here>";
            //    });

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

            services.AddControllersWithViews();
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

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
