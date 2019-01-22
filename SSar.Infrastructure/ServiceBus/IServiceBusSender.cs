using System.Threading.Tasks;
using SSar.Infrastructure.IntegrationEvents;

namespace SSar.Infrastructure.ServiceBus
{
    public interface IServiceBusSender
    {
        Task SendAsync(IIntegrationEvent @event);
    }
}