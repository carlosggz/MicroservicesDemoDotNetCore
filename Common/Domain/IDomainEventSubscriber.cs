using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    public interface IDomainEventSubscriber<T> where T : IDomainEvent
    {
        Task Dispatch(T domainEvent);
    }
}
