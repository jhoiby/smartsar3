using System.Collections.Generic;
using SSar.Infrastructure.DomainEvents;

namespace SSar.Infrastructure.Entities
{
    public interface IAggregateRoot : IEntity
    {
        ICollection<IDomainEvent> Events { get; }
    }
}