using System;
using System.Collections.Generic;
using System.Text;

namespace SSar.Contexts.Common.Validation
{
    public class DomainRequirementValidatorException : Exception
    {
        public DomainRequirementValidatorException()
            : base()
        {
        }

        public DomainRequirementValidatorException(string message)
            : base(message)
        {
        }

        public DomainRequirementValidatorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
