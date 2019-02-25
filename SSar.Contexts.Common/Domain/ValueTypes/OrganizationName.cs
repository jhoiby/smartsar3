using System;
using System.Collections.Generic;
using System.Text;
using SSar.Contexts.Common.Helpers;

namespace SSar.Contexts.Common.Domain.ValueTypes
{
    public class OrganizationName
    {
        public OrganizationName(string name)
        {
            Name = name.Require(nameof(name)).Trim();
        }

        public string Name { get; }

        public override string ToString() => Name;
    }
}
