using ActorsApi.Domain;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ActorsApi.Application
{
    public class GetActorsQueryHandler : IRequestHandler<GetActorsQuery, IEnumerable<ActorDto>>
    {
        private readonly IActorsRepository _repository;

        public GetActorsQueryHandler(IActorsRepository repository)
            => _repository = repository;

        public Task<IEnumerable<ActorDto>> Handle(GetActorsQuery request, CancellationToken cancellationToken)
            => Task.FromResult(_repository.GetAll());
    }
}
