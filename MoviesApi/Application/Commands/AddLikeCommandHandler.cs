using Common.Domain;
using MediatR;
using MoviesApi.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace MoviesApi.Application.Commands
{
    public class AddLikeCommandHandler : IRequestHandler<AddLikeCommand, bool>
    {
        private readonly IMoviesRepository _repository;
        private readonly IEventBus _eventBus;

        public AddLikeCommandHandler(IMoviesRepository repository, IEventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public Task<bool> Handle(AddLikeCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(request.Id))
                    return false;

                var entity = _repository.GetById(request.Id);

                if (entity == null)
                    return false;

                entity.Likes++;
                _repository.Update(entity);

                _eventBus.Record(new LikeAddedEvent(entity));
                _eventBus.PublishAsync();

                return true;
            });
        }
    }
}
