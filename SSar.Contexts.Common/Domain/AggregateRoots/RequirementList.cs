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

        public RequirementList AddNotificationRequirement(Func<bool> test, string paramName, string userMessage)
        {
            this.Add(new AggregateRequirement(test, paramName, userMessage)); 

            return this;
        }

        public RequirementList AddExceptionRequirement(Func<bool> test, string paramName, string userMessage, Exception exception)
        {
            this.Add(new AggregateRequirement(test, paramName, userMessage, exception));

            return this;
        }
    }
}
