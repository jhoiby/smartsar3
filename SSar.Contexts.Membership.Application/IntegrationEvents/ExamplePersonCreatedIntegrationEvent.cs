using System;
using SSar.Contexts.Common.Events;
using SSar.Contexts.Membership.Domain;
using SSar.Contexts.Membership.Domain.AggregateRoots.ExamplePerson;

namespace SSar.Contexts.Membership.Application.IntegrationEvents
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
