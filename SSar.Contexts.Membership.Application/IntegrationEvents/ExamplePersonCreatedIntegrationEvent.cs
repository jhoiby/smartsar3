using System;
using SSar.Contexts.Common.Application.IntegrationEvents;

namespace SSar.Contexts.Membership.Application.IntegrationEvents
{
    [Serializable]
    public class ExamplePersonCreatedIntegrationEvent : IntegrationEvent
    {
        public ExamplePersonCreatedIntegrationEvent(Guid id, string name, string emailAddress)
            : base(MembershipBoundedContextInfo.Name, nameof(ExamplePersonCreatedIntegrationEvent))
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
