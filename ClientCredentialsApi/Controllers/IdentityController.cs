using System.Linq;
using ClientCredentialsApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientCredentialsApi.Controllers
{
    [Route("identity")]
    
    public class IdentityController : Controller
    {
        /// <summary>
        /// 不传token 401
        /// 传了token  scope 不对 403 forbidden
        /// 传了token  scope 对  返回值
        /// </summary>
        /// <returns></returns>
        [Authorize(PolicyConstant.ApiScope)]
        [HttpGet]
        [Route("info")]
        public IActionResult Index()
        {
            return new JsonResult(from c in User.Claims select new {c.Type,c.Value});
        }

        /// <summary>
        /// 不传token 401
        /// 传了 token就可以获取值
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("InfoOne")]
        [Authorize]
        public string InfoOne()
        {
            return "One";
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("InfoTwo")]
        public string InfoTwo()
        {
            return "Two";
        }
    }
}