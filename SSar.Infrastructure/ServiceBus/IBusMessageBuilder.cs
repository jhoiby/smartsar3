using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.ServiceBus;
using SSar.Infrastructure.IntegrationEvents;

namespace SSar.Infrastructure.ServiceBus
{
    public interface IBusMessageBuilder<TObject, out TMessage>
        where TObject : IIntegrationEvent
        where TMessage : Message
    {
        IBusMessageBuilder<TObject, TMessage> WithObject(TObject @event);
        TMessage Build();
    }
}
