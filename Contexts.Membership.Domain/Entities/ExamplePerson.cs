using System;
using System.Runtime.CompilerServices;
using SSar.Contexts.Common.AbstractClasses;
using SSar.Contexts.Common.Helpers;
using SSar.Contexts.Common.Notifications;

namespace SSar.Contexts.Membership.Domain.Entities
{
    public class ExamplePerson : AggregateRoot
    {
        public string _name;
        public string _emailAddress;

        private ExamplePerson()
        {
        }
        
        public string Name => _name;
        public string EmailAddress => _emailAddress;

        public static AggregateResult<ExamplePerson> Create(string name, string emailAddress)
        {
            var person = new ExamplePerson();

            person.SetName(name);
            person.SetEmailAddress(emailAddress);

            return AggregateResult<ExamplePerson>.FromAggregate(person);
        }

        public AggregateResult<ExamplePerson> SetName(string name)
        {
            _name = name;

            return AggregateResult<ExamplePerson>.FromAggregate(this);
        }

        public AggregateResult<ExamplePerson> SetEmailAddress(string emailAddress)
        {
            _emailAddress = emailAddress;

            return AggregateResult<ExamplePerson>.FromAggregate(this);
        }
    }
}
