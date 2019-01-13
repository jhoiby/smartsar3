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
            throw new NotImplementedException();
        }

        public AggregateResult<ExamplePerson> SetName(string name)
        {
            throw new NotImplementedException();
        }

        public AggregateResult<ExamplePerson> SetEmailAddress(string emailAddress)
        {
            throw new NotImplementedException();
        }
    }
}
