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

        public LikeEventSubscriber(ILogger<LikeEventSubscriber> logger)
        {
            _logger = logger;
        }

        public Task Dispatch(LikeAddedEvent domainEvent)
        {
            _logger.LogInformation("Processing message " + domainEvent.AggregateRootId);
            return Task.CompletedTask;
        }
    }
}
