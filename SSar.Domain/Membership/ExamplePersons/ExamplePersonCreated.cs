using System;
using SSar.Infrastructure.DomainEvents;

namespace SSar.Domain.Membership.ExamplePersons
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
