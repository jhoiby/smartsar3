using System;
using SSar.Contexts.Common.Events;

namespace SSar.Contexts.Membership.Domain.AggregateRoots.ExamplePerson
{
    [Serializable]
    public class ExamplePersonCreated : DomainEvent
    {
        public ExamplePersonCreated(Guid id, string name, string emailAddress)
            :base(BoundedContextInfo.Name, nameof(ExamplePerson), nameof(ExamplePersonCreated))
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
