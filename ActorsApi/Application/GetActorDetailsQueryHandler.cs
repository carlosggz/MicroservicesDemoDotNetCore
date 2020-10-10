using ActorsApi.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ActorsApi.Application
{
    public class GetActorDetailsQueryHandler : IRequestHandler<GetActorDetailsQuery, ActorEntity>
    {
        private readonly IActorsRepository _repository;

        public GetActorDetailsQueryHandler(IActorsRepository repository)
            => _repository = repository;

        public Task<ActorEntity> Handle(GetActorDetailsQuery request, CancellationToken cancellationToken)
            => Task.FromResult(_repository.GetById(request.Id));
    }
}
