using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthApi.Application;
using AuthApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginModel model)
        {
            var token = await _mediator.Send(new GenerateUserTokenCommand(model.Login, model.Password));

            if (string.IsNullOrEmpty(token))
                return BadRequest();

            return Ok(token);
        }
    }
}
