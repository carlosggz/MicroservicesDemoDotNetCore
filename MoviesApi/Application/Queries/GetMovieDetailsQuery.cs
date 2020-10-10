using MediatR;
using MoviesApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesApi.Application.Queries
{
    public class GetMovieDetailsQuery: IRequest<MovieEntity>
    {
        public string Id { get; private set; }

        public GetMovieDetailsQuery(string id)
            => Id = id;
    }
}
