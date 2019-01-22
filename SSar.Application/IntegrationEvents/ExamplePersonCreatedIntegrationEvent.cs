using System;
using SSar.Domain.Infrastructure;

namespace SSar.Application.IntegrationEvents
{
    [Serializable]
    public class ExamplePersonCreatedIntegrationEvent : DomainEvent
    {
        public ExamplePersonCreatedIntegrationEvent(Guid id, string name, string emailAddress)
        {
            Id = id;
            Name = name;
            EmailAddress = emailAddress;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string EmailAddress { get; }
    }
}
