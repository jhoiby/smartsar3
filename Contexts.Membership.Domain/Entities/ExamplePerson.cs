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

            aggregate.SetName(name);
            aggregate.SetEmailAddress(emailAddress);

            return aggregate;
        }

        public string Name => _name;
        public string EmailAddress => _emailAddress;


        public OperationResult SetName(string name)
        {
            // TODO: Create a method like ValidateAgainstRules(Predicate[], nameof(name))
            // where List<Predicate> is an array of lambdas expressing rules to be tested against
            // The lambda would contain the test and the notification value. Also consider
            // being able to throw exceptions.
            // OR: Build a set of specifications somewhere and run through them here.

            if (name == null)
            {
                throw new ArgumentNullException(nameof(Name));
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
            
            return OperationResult.CreateSuccessful();
        }

        public void SetEmailAddress(string emailAddress)
        {
            _emailAddress = emailAddress;
        }

    }
}
