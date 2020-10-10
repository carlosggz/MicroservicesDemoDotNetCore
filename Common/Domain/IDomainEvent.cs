using System;

namespace Common.Domain
{
    public interface IDomainEvent
    {
        string EventId { get; }
        string AggregateRootId { get; }
        DateTime OccurrenceDate { get; }
    }
}
