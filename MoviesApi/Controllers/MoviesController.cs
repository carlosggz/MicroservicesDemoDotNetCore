using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
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

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<MovieEntity>> GetById([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var movie = await _mediator.Send(new GetMovieDetailsQuery(id));

            if (movie == null)
                return BadRequest();
            
            return Ok(movie);
        }

        [Route("likes/{id}")]
        [HttpGet]
        public async Task<ActionResult> AddLikes([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var result = await _mediator.Send(new AddLikeCommand(id));

            if (!result)
                return BadRequest();

            return Ok();
        }
    }
}
