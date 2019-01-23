using System;
using System.Collections.Generic;
using System.Text;
using SSar.Data.Outbox;

namespace SSar.Infrastructure.IntegrationEvents
{
    public interface IIntegrationEventQueue : IList<IIntegrationEvent>
    {
    }
}
