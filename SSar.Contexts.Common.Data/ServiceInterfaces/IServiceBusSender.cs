using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.Azure.Amqp;
using SSar.Contexts.Common.Application;
using SSar.Contexts.Common.Application.IntegrationEvents;

namespace SSar.Contexts.Common.Data.ServiceInterfaces
{
    public interface IServiceBusSender<TObject> where TObject : IBusPublishable
    {
        Task SendAsync(TObject @event);
    }
}