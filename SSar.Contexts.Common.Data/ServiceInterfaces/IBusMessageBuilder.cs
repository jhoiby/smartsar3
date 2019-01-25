using System.Runtime.Serialization;
using Microsoft.Azure.ServiceBus;
using SSar.Contexts.Common.Application;
using SSar.Contexts.Common.Application.IntegrationEvents;

namespace SSar.Contexts.Common.Data.ServiceInterfaces
{
    public interface IBusMessageBuilder<TObject, out TMessage>
        where TObject : IBusPublishable
        where TMessage : Message
    {
        IBusMessageBuilder<TObject, TMessage> WithObject(TObject @event);
        TMessage Build();
    }
}
