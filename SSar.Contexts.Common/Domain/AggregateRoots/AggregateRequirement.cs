using System;

namespace SSar.Contexts.Common.Domain.AggregateRoots
{
    public class AggregateRequirement
    {
        public AggregateRequirement(Func<bool> test, string paramName, string failureMessage, Exception exception = null)
        {
            Test = test;
            ParamName = paramName ?? "";
            FailureMessage = failureMessage ?? "";
            Exception = exception;
        }

        public Func<bool> Test { get; }
        public string ParamName { get; }
        public string FailureMessage { get; }
        public Exception Exception { get; }
    }
}
