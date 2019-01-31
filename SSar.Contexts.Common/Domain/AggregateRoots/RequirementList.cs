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

        public RequirementList AddRequirement(Func<bool> test, string paramName, string userMessage)
        {
            this.Add(new AggregateRequirement(test, paramName, userMessage)); 

            return this;
        }

        public RequirementList AddException(Func<bool> test, string paramName, string userMessage, Exception exception)
        {
            this.Add(new AggregateRequirement(test, paramName, userMessage, exception));

            return this;
        }
    }
}
