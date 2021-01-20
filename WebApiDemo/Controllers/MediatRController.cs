using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.MediatR.Notification;
using WebApiDemo.MediatR.Request;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediatRController
    {
        private readonly IMediator _mediator;

        public MediatRController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("user")]
        public async Task<string> CreateUserAsync([FromQuery] string name)
        {
            var response = await _mediator.Send(new CreateUserCommand() {Name = name});
            return response;
        }

        [HttpGet("publish")]
        public async Task Publish()
        {
             await _mediator.Publish(new SomeEvent("Hello World!"));
        }
    }
}
