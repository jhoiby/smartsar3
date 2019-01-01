using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SSar.Contexts.Common.Validation
{
    public class DomainRuleRunnerException : Exception
    {
        public DomainRuleRunnerException()
        {
        }

        public DomainRuleRunnerException(string message) 
            : base(message)
        {
        }

        public DomainRuleRunnerException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
