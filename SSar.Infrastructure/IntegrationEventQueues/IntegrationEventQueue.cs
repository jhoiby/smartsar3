using System.Collections.Generic;
using SSar.Contexts.Common.Application.IntegrationEvents;
using SSar.Contexts.Common.Application.ServiceInterfaces;

namespace SSar.Infrastructure.IntegrationEventQueues
{
    // TODO: Consider what the lifetime scope of this needs to be for DI injection
    // Probably Singleton or per-session.

    public class IntegrationEventQueue : List<IIntegrationEvent>, IIntegrationEventQueue
    {
    }
}
