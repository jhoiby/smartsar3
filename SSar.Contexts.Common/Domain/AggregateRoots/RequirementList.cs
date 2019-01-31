using System;
using System.Collections.Generic;

namespace SSar.Contexts.Common.Domain.AggregateRoots
{
    public class RequirementList : List<AggregateRequirement>
    {
        private RequirementList()
        {
        }

        public static RequirementList Create()
        {
            return new RequirementList();
        }

        public RequirementList AddRequirement(Func<bool> test, string paramName, string failureMessage)
        {
            var requirementSet = new AggregateRequirement(test, paramName, failureMessage);

            this.Add(requirementSet);

            return this;
        }
    }
}
