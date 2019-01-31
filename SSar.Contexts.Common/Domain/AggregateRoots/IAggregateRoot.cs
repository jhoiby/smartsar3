using System.Collections.Generic;
using SSar.Contexts.Common.Domain.DomainEvents;
using SSar.Contexts.Common.Domain.Entities;

namespace SSar.Contexts.Common.Domain.AggregateRoots
{
    public interface IAggregateRoot : IEntity
    {
        ICollection<IDomainEvent> Events { get; }
    }
}