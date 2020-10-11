using Common.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActorsApi.Domain
{
    public class LikeEventSubscriber : IDomainEventSubscriber<LikeAddedEvent>
    {
        private readonly ILogger<LikeEventSubscriber> _logger;
        private readonly IActorsRepository _repository;

        public LikeEventSubscriber(ILogger<LikeEventSubscriber> logger, IActorsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public Task Dispatch(LikeAddedEvent domainEvent)
        {
            _logger.LogInformation("Processing message " + domainEvent.AggregateRootId);

            var actors = _repository.Search(domainEvent.AggregateRootId);

            foreach (var actor in actors)
            {
                actor.Likes++;
                _repository.Update(actor);
            }

            return Task.CompletedTask;
        }
    }
}
