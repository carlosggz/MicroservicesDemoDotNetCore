using MediatR;
using MoviesApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MoviesApi.Application.Queries
{
    public class SearchQueryHandler : IRequestHandler<SearchQuery, IEnumerable<MovieDto>>
    {
        private readonly IMoviesRepository _repository;

        public SearchQueryHandler(IMoviesRepository repository)
            => _repository = repository;

        public Task<IEnumerable<MovieDto>> Handle(SearchQuery request, CancellationToken cancellationToken)
        {
            return Task.Run(() => 
            {
                return (request.Ids == null || !request.Ids.Any())
                    ? new List<MovieDto>()
                    : _repository.Search(request.Ids);
            });
           
        }
    }
}
