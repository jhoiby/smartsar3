using System.Collections;
using System.Collections.Generic;
using SSar.Contexts.Common.Events;

namespace SSar.Contexts.Common.Entities
{
    public interface IAggregateRoot : IEntity
    {
        ICollection<IDomainEvent> Events { get; }
    }
}