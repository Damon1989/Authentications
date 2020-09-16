using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Basics.CustomPolicyProvider;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basics.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IAuthorizationService _authorizationService;

        //public HomeController(IAuthorizationService authorizationService)
        //{
        //    _authorizationService = authorizationService;
        //}
        // GET
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        [Authorize(Policy = "Claim.DoB")]
        public IActionResult SecretPolicy()
        {
            return View(nameof(Secret));
        }


        //[Authorize(Roles = "Admin")]
        [Authorize(Policy = "Admin")]
        public IActionResult SecretRole()
        {
            return View(nameof(Secret));
        }


        [SecurityLevel(5)]
        public IActionResult SecretLevel()
        {
            return View(nameof(Secret));
        }

        [SecurityLevel(10)]
        public IActionResult SecretHigherLevel()
        {
            return View(nameof(Secret));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Authenticate()
        {
            var grandmaClaims=new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Damon"),
                new Claim(ClaimTypes.Email,"Damon@qq.com"),
                new Claim(ClaimTypes.DateOfBirth,"11/11/2016"),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.Role,"AdminTwo"),
                new Claim(DynamicPolicies.SecurityLevel,"7"),
                new Claim("Grandma.Says","Very nice boi.")
            };

            var licenseClaims=new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Andy"),
                new Claim("DrivingLicense","A+")
            };

            var grandmaIdentity=new ClaimsIdentity(grandmaClaims,"Grandma Identity");
            var licenseIdentity = new ClaimsIdentity(licenseClaims, "Government");

            var userPrincipal=new ClaimsPrincipal(new []{grandmaIdentity, licenseIdentity });

            await HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DoStuff([FromServices] IAuthorizationService authorizationService)
        {
            // we are doing stuff here

            var builder=new AuthorizationPolicyBuilder("Schema");
            var customPolicy = builder.RequireClaim("Hello").Build();

            //await _authorizationService.AuthorizeAsync(User, "Claim.DoB");
            var authResult= await authorizationService.AuthorizeAsync(User, customPolicy);
            if (authResult.Succeeded)
            {
                return View(nameof(Index));
            }
            return View(nameof(Index));
        }
    }
}