using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActorsApi.Domain
{
    public class LikeAddedEvent : IDomainEvent
    {
        public string Title { get; set; }

        public string EventId { get; set; }

        public string AggregateRootId { get; set; }

        public DateTime OccurrenceDate { get; set; }
    }
}
