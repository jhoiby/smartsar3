using System.Collections.Generic;
using SSar.Contexts.Common.Domain.DomainEvents;

namespace SSar.Contexts.Common.Domain.Entities
{
    public interface IAggregateRoot : IEntity
    {
        ICollection<IDomainEvent> Events { get; }
    }
}