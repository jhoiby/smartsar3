using System.Threading.Tasks;
using SSar.Infrastructure.IntegrationEvents;

namespace SSar.Infrastructure.ServiceBus
{
    public interface IServiceBusSender
    {
        // TODO: Return completion status of send
        Task SendAsync(IIntegrationEvent @event);
    }
}