using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SSar.Contexts.Entities.DomainEvents;
using SSar.Contexts.Membership.Domain.Entities;

namespace SSar.Contexts.Membership.Domain.DomainEvents
{
    public class ExamplePersonCreated : IDomainEvent
    {
        public ExamplePersonCreated(
            ExamplePerson examplePerson, 
            string description = "A new ExamplePerson was added.")
        {
            EventId = Guid.NewGuid();
            ExamplePerson = examplePerson;
            Description = description;
        }

        public Guid EventId { get; }
        public ExamplePerson ExamplePerson { get; }
        public string Description { get; }
    }
}
