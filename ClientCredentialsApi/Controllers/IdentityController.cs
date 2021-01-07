using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientCredentialsApi.Controllers
{
    [Route("identity")]
    [Authorize]
    public class IdentityController : Controller
    {
        [HttpGet]
        [Route("info")]
        public IActionResult Index()
        {
            return new JsonResult(from c in User.Claims select new {c.Type,c.Value});
        }
    }
}