using System;
using SSar.Contexts.Common.Domain.DomainEvents;

namespace SSar.Contexts.Membership.Domain.AggregateRoots.ExamplePersons
{
    [Serializable]
    public class ExamplePersonCreated : DomainEvent
    {
        public ExamplePersonCreated(Guid id, string name, string emailAddress)
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
