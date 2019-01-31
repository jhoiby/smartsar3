using System;

namespace SSar.Contexts.Common.Domain.AggregateRoots
{
    public class AggregateRequirement
    {
        public AggregateRequirement(Func<bool> test, string paramName, string failureMessage)
        {
            Test = test;
            ParamName = paramName;
            FailureMessage = failureMessage;
        }

        public Func<bool> Test { get; }
        public string ParamName { get; }
        public string FailureMessage { get; }
    }
}
