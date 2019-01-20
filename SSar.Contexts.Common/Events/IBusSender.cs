using System.Threading.Tasks;

namespace SSar.Contexts.Common.Events
{
    public interface IBusSender
    {
        Task SendAsync(IIntegrationEvent @event);
    }
}