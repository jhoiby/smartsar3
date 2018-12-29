using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Contexts.Membership.Domain.Entities
{
    public class ExamplePersonAggregate
    {
        public string _name;
        public string _emailAddress;

        private ExamplePersonAggregate()
        {
        }

        public static ExamplePersonAggregate CreateFromData(string name, string emailAddress)
        {
            var aggregate = new ExamplePersonAggregate();

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
