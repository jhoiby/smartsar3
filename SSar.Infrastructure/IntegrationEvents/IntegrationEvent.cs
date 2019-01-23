using System;
using System.Runtime.Serialization;

namespace SSar.Infrastructure.IntegrationEvents
{
    [Serializable]
    public abstract class IntegrationEvent : IIntegrationEvent
    {
        private readonly Guid _eventId = Guid.NewGuid();
        private readonly string _label;
        private readonly string _publisher;
        private readonly DateTime _occurredAtUtc = DateTime.UtcNow;

        protected IntegrationEvent(string boundedContextName, string eventName)
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