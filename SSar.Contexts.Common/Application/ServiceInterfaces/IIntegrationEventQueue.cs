using System.Collections.Generic;
using SSar.Contexts.Common.Application.IntegrationEvents;

namespace SSar.Contexts.Common.Application.ServiceInterfaces
{
    public interface IIntegrationEventQueue : IList<IIntegrationEvent>
    {
    }
}
