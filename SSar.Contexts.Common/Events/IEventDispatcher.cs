using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SSar.Contexts.Common.Entities;

namespace SSar.Contexts.Common.Events
{
    public interface IEventDispatcher
    {
        Task<IAggregateRoot[]> DispatchInternalBoundedContextEventsAsync(IAggregateRoot[] aggregates);
        Task<IAggregateRoot[]> PublishToIntegrationBusAsync(IAggregateRoot[] aggregates);
        IAggregateRoot[] ClearEventEntities(IAggregateRoot[] aggregates);
    }
}
