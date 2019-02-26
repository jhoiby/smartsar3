using System;
using System.Collections.Generic;
using System.Text;
using SSar.Contexts.Common.Helpers;

namespace SSar.Contexts.Common.Domain.ValueTypes
{
    public class PersonName
    {
        public PersonName(string firstName, string lastName, string nickname)
        {
            FirstName = firstName.Require(nameof(firstName)).Trim();
            LastName = lastName.Require(nameof(lastName)).Trim();
            Nickname = nickname.Require(nameof(nickname)).Trim();
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Nickname { get; private set; }
        public string FullName => FirstName + " " + LastName;
    }
}
