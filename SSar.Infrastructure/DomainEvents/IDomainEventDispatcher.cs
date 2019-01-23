using System.Threading.Tasks;
using SSar.Infrastructure.Entities;

namespace SSar.Infrastructure.DomainEvents
{
    public interface IDomainEventDispatcher
    {
        Task<IAggregateRoot[]> DispatchAndClearDomainEventsAsync(IAggregateRoot[] aggregates);
    }
}
