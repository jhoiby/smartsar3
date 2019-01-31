using System.Threading.Tasks;
using SSar.Contexts.Common.Domain.AggregateRoots;
using SSar.Contexts.Common.Domain.Entities;

namespace SSar.Contexts.Common.Domain.ServiceInterfaces
{
    public interface IDomainEventDispatcher
    {
        Task<IAggregateRoot[]> DispatchAndClearDomainEventsAsync(IAggregateRoot[] aggregates);
    }
}
