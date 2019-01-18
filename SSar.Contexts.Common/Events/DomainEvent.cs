using System;

namespace SSar.Contexts.Common.Events
{
    public abstract class DomainEvent : IDomainEvent
    {
        private readonly Guid _eventId = Guid.NewGuid();
        private readonly DateTime _occurredAt = DateTime.UtcNow;

        public Guid EventId => _eventId;
        public DateTime OccurredAt => _occurredAt;
        public int EventVersion => 1;

        public abstract string Label { get; }
    }
}