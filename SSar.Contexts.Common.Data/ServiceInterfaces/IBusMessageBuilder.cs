using Microsoft.Azure.ServiceBus;
using SSar.Contexts.Common.Application.IntegrationEvents;

namespace SSar.Contexts.Common.Data.ServiceInterfaces
{
    public interface IBusMessageBuilder<TObject, out TMessage>
        where TObject : IIntegrationEvent
        where TMessage : Message
    {
        IBusMessageBuilder<TObject, TMessage> WithObject(TObject @event);
        TMessage Build();
    }
}
