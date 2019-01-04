using System;
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

        public static ExamplePerson CreateFromData(string name, string emailAddress)
        {
            var aggregate = new ExamplePerson();

            aggregate.SetName(name);
            aggregate.SetEmailAddress(emailAddress);

            return aggregate;
        }

        public OperationResult SetName(string name)
        {
            name.ThrowIfArgumentNull(nameof(name));

            _name = name;

            return OperationResult.CreateSuccessful();
        }

        public OperationResult SetEmailAddress(string emailAddress)
        {
            emailAddress.ThrowIfArgumentNull(nameof(emailAddress));

            _emailAddress = emailAddress;

            return OperationResult.CreateSuccessful();
        }
    }
}
