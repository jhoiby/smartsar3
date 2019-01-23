using System;
using SSar.Infrastructure;
using SSar.Infrastructure.DomainEvents;
using SSar.Infrastructure.IntegrationEvents;

namespace SSar.Application.IntegrationEvents
{
    [Serializable]
    public class ExamplePersonCreatedIntegrationEvent : IntegrationEvent
    {
        public ExamplePersonCreatedIntegrationEvent(Guid id, string name, string emailAddress)
            : base(ApplicationInfo.Name, nameof(ExamplePersonCreatedIntegrationEvent))
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
