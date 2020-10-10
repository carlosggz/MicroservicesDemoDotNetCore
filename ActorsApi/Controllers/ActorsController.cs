using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActorsApi.Application;
using ActorsApi.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ActorsApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ActorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetAll()
        {
            var actors = await _mediator.Send(new GetActorsQuery());
            return Ok(actors);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<ActorEntity>> GetById([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var actor = await _mediator.Send(new GetActorDetailsQuery(id));

            if (actor == null)
                return BadRequest();

            return Ok(actor);
        }
    }
}
