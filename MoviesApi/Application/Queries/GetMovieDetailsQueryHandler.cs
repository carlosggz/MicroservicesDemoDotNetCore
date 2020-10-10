using MediatR;
using MoviesApi.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace MoviesApi.Application.Queries
{
    public class GetMovieDetailsQueryHandler : IRequestHandler<GetMovieDetailsQuery, MovieEntity>
    {
        private readonly IMoviesRepository _repository;

        public GetMovieDetailsQueryHandler(IMoviesRepository repository)
            => _repository = repository;

        public Task<MovieEntity> Handle(GetMovieDetailsQuery request, CancellationToken cancellationToken)
            => Task.FromResult(_repository.GetById(request.Id));
    }
}
