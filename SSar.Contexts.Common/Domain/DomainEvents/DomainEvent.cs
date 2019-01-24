using System;

namespace SSar.Contexts.Common.Domain.DomainEvents
{
    [Serializable]
    public abstract class DomainEvent : IDomainEvent
    {
        private readonly Guid _eventId = Guid.NewGuid();
        private readonly DateTime _occurredAtUtc = DateTime.UtcNow;

        public Guid EventId => _eventId;
        public DateTime OccurredAtUtc => _occurredAtUtc;
        public int EventVersion => 1;
    }
}