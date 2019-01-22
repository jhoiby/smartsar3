using System;
using System.Collections.Generic;

namespace SSar.Domain.Infrastructure
{
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        private List<IDomainEvent> _events = new List<IDomainEvent>();

        protected AggregateRoot()
        {
        }

        protected AggregateRoot(Guid id) : base(id)
        {
        }

        public ICollection<IDomainEvent> Events => _events;

        protected void AddEvent(IDomainEvent @event)
        {
            _events.Add(@event);
        }

        protected void ClearEvents()
        {
            _events.Clear();
        }
    }
}
