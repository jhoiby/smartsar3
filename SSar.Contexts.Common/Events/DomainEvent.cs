using System;

namespace SSar.Contexts.Common.Events
{
    [Serializable]
    public abstract class DomainEvent : IDomainEvent
    {
        private readonly Guid _eventId = Guid.NewGuid();
        private readonly string _label;
        private readonly string _publisher;
        private readonly DateTime _occurredAtUtc = DateTime.UtcNow;

        protected DomainEvent(string boundedContextName, string sourceAggregateName, string eventName)
        {
            _label = eventName;
            _publisher = $"{boundedContextName}";
        }

        public Guid EventId => _eventId;
        public DateTime OccurredAtUtc => _occurredAtUtc;
        public int EventVersion => 1;
        public string Label => _label;
        public string Publisher => _publisher;
    }
}