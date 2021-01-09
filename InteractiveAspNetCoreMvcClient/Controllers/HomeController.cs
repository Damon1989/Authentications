using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using InteractiveAspNetCoreMvcClient.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace InteractiveAspNetCoreMvcClient.Controllers
{
    public class HomeController:Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// the identity token containing the information about the authentication and session
        /// the access token to access APIs on behalf of the logged on user.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> UserInfo()
        {
            var accessToken=await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            var idToken= await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);
            var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
            ViewBag.AccessToken = accessToken;
            ViewBag.IdToken = idToken;
            ViewBag.RefreshToken = refreshToken;
            var client=new HttpClient();
            //client.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer",accessToken);
            client.SetBearerToken(accessToken);
            
            var response= await client.GetAsync("http://localhost:5001/connect/userinfo");
            var result = "";
            if (!response.IsSuccessStatusCode)
            {
                result = response.StatusCode.ToString();
            }
            else
            {
                result = await response.Content.ReadAsStringAsync();
            }

            ViewBag.Result = result;
            return View();
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
