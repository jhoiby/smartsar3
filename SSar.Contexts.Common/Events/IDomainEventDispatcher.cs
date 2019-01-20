using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SSar.Contexts.Common.Entities;

namespace SSar.Contexts.Common.Events
{
    public interface IDomainEventDispatcher
    {
        Task<IAggregateRoot[]> DispatchInternalBoundedContextEventsAsync(IAggregateRoot[] aggregates);
        IAggregateRoot[] ClearEventEntities(IAggregateRoot[] aggregates);
    }
}
