using System.Collections;
using System.Collections.Generic;
using SSar.Contexts.Entities.DomainEvents;

namespace SSar.Contexts.Common.Entities
{
    public interface IAggregateRoot : IEntity
    {
        ICollection<IDomainEvent> Events { get; }
    }
}