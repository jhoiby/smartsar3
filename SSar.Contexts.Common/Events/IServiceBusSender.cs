using System.Threading.Tasks;

namespace SSar.Contexts.Common.Events
{
    public interface IServiceBusSender
    {
        Task SendAsync(IIntegrationEvent @event);
    }
}