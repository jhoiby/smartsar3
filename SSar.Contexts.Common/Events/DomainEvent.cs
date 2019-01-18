using System;

namespace SSar.Contexts.Common.Events
{
    public abstract class DomainEvent : IDomainEvent
    {
        private readonly Guid _eventId = Guid.NewGuid();
        private readonly DateTime _occurredAtUtc = DateTime.UtcNow;

        public Guid EventId => _eventId;
        public DateTime OccurredAtUtc => _occurredAtUtc;
        public int EventVersion => 1;

        public abstract string Label { get; }        // For integration service bus Message.Label
        public abstract string Publisher { get; }    // Name of application or BC, i.e. "Membership"
    }
}