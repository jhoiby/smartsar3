using System.Threading.Tasks;

namespace SSar.Contexts.Common.Events
{
    public interface IIntegrationBusService
    {
        Task SendAsync(IDomainEvent @event);
    }
}