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

        public static ExamplePerson CreateFromData(string name, string emailAddress)
        {
            var aggregate = new ExamplePerson();
            var finalResult = OperationResult.CreateEmpty();

            aggregate.SetName(name).AppendErrorsTo(finalResult);
            aggregate.SetEmailAddress(emailAddress).AppendErrorsTo(finalResult);

            return aggregate;
        }

        public string Name => _name;
        public string EmailAddress => _emailAddress;

        public OperationResult SetName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            if (name.Length == 0)
            {
                AddError(nameof(Name), "Name is required.");
            }

            if (HasErrors)
            {
                return OperationResult.CreateFromErrors(Errors);
            }

            _name = name;
            
            return OperationResult.CreateEmpty();
        }

        public OperationResult SetEmailAddress(string emailAddress)
        {
            if (emailAddress == null)
            {
                throw new ArgumentNullException(nameof(emailAddress));
            }

            if (emailAddress.Length == 0)
            {
                AddError(nameof(EmailAddress), "Email address is required.");
            }

            if (HasErrors)
            {
                return OperationResult.CreateFromErrors(Errors);
            }

            _emailAddress = emailAddress;

            return OperationResult.CreateEmpty();
        }
    }
}
