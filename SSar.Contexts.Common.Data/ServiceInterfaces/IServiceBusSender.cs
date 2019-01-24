using System.Threading.Tasks;
using SSar.Contexts.Common.Application.IntegrationEvents;

namespace SSar.Contexts.Common.Data.ServiceInterfaces
{
    public interface IServiceBusSender
    {
        // TODO: Return completion status of send
        Task SendAsync(IIntegrationEvent @event);
    }
}