using System;
using SSar.Contexts.Common.AbstractClasses;
using SSar.Contexts.Common.Results;
using SSar.Contexts.Common.Validation;

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
            var finalResult = OperationResult.CreateEmpty();

            aggregate.SetName(name).AppendErrorsTo(finalResult);
            aggregate.SetEmailAddress(emailAddress).AppendErrorsTo(finalResult);

            return aggregate;
        }

        public OperationResult SetName(string name)
        {
            return OperationResult.CreateEmpty();
        }

        public OperationResult SetEmailAddress(string emailAddress)
        {
            return OperationResult.CreateEmpty();
        }
    }
}
