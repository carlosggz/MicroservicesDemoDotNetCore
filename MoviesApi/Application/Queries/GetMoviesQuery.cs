using MediatR;
using MoviesApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesApi.Application.Queries
{
    public class GetMoviesQuery: IRequest<IEnumerable<MovieDto>>
    {}
}
