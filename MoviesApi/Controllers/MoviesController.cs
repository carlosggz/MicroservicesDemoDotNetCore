using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Application.Commands;
using MoviesApi.Application.Queries;
using MoviesApi.Domain;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetAll()
        {
            var movies = await _mediator.Send(new GetMoviesQuery());
            return Ok(movies);
        }

        [Authorize]
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<MovieEntity>> GetById([FromRoute] string id)
        {
            var movie = await _mediator.Send(new GetMovieDetailsQuery(id));

            if (movie == null)
                return BadRequest();
            
            return Ok(movie);
        }

        [Authorize]
        [Route("like/{id}")]
        [HttpGet]
        public async Task<ActionResult> AddLikes([FromRoute] string id)
        {
            var result = await _mediator.Send(new AddLikeCommand(id));

            if (!result)
                return BadRequest();

            return Ok();
        }

        [Route("search")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<MovieDto>>> Search([FromBody] string[] ids)
        { 
            var result = await _mediator.Send(new SearchQuery(ids));
            return Ok(result);
        }
    }
}
