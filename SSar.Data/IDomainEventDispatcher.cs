using System.Threading.Tasks;
using SSar.Domain.Infrastructure;

namespace SSar.Data
{
    public interface IDomainEventDispatcher
    {
        Task<IAggregateRoot[]> DispatchInternalBoundedContextEventsAsync(IAggregateRoot[] aggregates);
        IAggregateRoot[] ClearEventEntities(IAggregateRoot[] aggregates);
    }
}
