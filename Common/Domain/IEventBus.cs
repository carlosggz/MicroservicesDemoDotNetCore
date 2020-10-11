using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    public interface IEventBus
    {
        void Record(IDomainEvent domainEvent);
        Task PublishAsync();
        void DiscardEvents();
        void Subscribe<E, H>()
            where E : IDomainEvent
            where H : IDomainEventSubscriber<E>;
    }
}
