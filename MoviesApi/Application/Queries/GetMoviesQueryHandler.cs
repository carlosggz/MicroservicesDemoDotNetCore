using MediatR;
using MoviesApi.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MoviesApi.Application.Queries
{
    public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, IEnumerable<MovieDto>>
    {
        private readonly IMoviesRepository _repository;

        public GetMoviesQueryHandler(IMoviesRepository repository) 
            => _repository = repository;

        public Task<IEnumerable<MovieDto>> Handle(GetMoviesQuery request, CancellationToken cancellationToken) 
            => Task.FromResult(_repository.GetAll());
    }
}
