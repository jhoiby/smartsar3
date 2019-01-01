using System;
using SSar.Contexts.Common.AbstractClasses;
using SSar.Contexts.Common.Results;

namespace SSar.Contexts.Membership.Domain.Entities
{
    // TODO: Complete notifications by returning an OperationResult object from methods

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
            var finalResult = OperationResult.CreateEmpty();

            aggregate.SetName(name).AppendErrorsTo(finalResult);
            aggregate.SetEmailAddress(emailAddress).AppendErrorsTo(finalResult);

            return aggregate;
        }

        public OperationResult SetName(string name)
        {
            ThrowIfNullParam(name, nameof(name));
            
            ValidateDomainRule( () => name.Length > 0, nameof(Name), "Name is required.");

            if (!HasErrors)
            {
                _name = name;
            }

            return Result();
        }

        public OperationResult SetEmailAddress(string emailAddress)
        {
            ThrowIfNullParam(emailAddress, nameof(emailAddress));
            
            ValidateDomainRule( 
                () => emailAddress.Length > 0, 
                nameof(EmailAddress), 
                "Email address is required.");

            if (!HasErrors)
            {
                _emailAddress = emailAddress;
            }

            return Result();
        }
    }
}
