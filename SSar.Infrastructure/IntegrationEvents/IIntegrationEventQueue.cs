using System;
using System.Collections.Generic;
using System.Text;

namespace SSar.Infrastructure.IntegrationEvents
{
    public interface IIntegrationEventQueue : IList<IIntegrationEvent>
    {
    }
}
