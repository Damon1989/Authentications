using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OcelotServer.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class OcelotController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await Task.Run(() =>
                $"This is from {HttpContext.Request.Host.Value}，path:{HttpContext.Request.Path}");
            return Ok(result);
        }

        //[Authorize("ApiScope")]
        //[Authorize]
        [HttpGet("aggrDamon")]
        public async Task<IActionResult> AggrDamon()
        {
            var result = await Task.Run(() => $"我是Damon,还是多加工资最实际，path:{HttpContext.Request.Path}");
            return Ok(new ResponseResult(){Comment = result});
        }

        [HttpGet("aggrDd")]
        public async Task<IActionResult> AggrDd()
        {
            var result = await Task.Run(() => $"我是Dd,我非常珍惜现在的工作机会，path:{HttpContext.Request.Path}");
            return Ok(new ResponseResult() { Comment = result });
        }
    }

    public class ResponseResult
    {
        public string Comment { get; set; }
    }
}
