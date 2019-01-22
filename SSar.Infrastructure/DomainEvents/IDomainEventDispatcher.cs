using System.Threading.Tasks;
using SSar.Infrastructure.Entities;

namespace SSar.Infrastructure.DomainEvents
{
    public interface IDomainEventDispatcher
    {
        Task<IAggregateRoot[]> DispatchDomainEventsAsync(IAggregateRoot[] aggregates);
        IAggregateRoot[] ClearEventEntities(IAggregateRoot[] aggregates);
    }
}
