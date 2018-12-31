using SSar.Contexts.Common.AbstractClasses;

namespace SSar.Contexts.Membership.Domain.Entities
{
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


        private void SetName(string name)
        {
            _name = name;
        }

        private void SetEmailAddress(string emailAddress)
        {
            _emailAddress = emailAddress;
        }

    }
}
