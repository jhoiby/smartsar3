using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSar.Infrastructure.DomainEvents;
using SSar.Infrastructure.ServiceBus;

namespace SSar.Infrastructure.IntegrationEvents
{
    // TODO: Consider what the lifetime scope of this needs to be for DI injection
    // Probably Singleton or per-session.

    public class IntegrationEventQueue : List<IIntegrationEvent>, IIntegrationEventQueue
    {
    }
}
