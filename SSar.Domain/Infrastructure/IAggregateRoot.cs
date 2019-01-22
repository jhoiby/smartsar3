using System.Collections.Generic;

namespace SSar.Domain.Infrastructure
{
    public interface IAggregateRoot : IEntity
    {
        ICollection<IDomainEvent> Events { get; }
    }
}