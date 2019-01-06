using System;
using System.Collections.Generic;
using System.Text;

namespace SSar.Contexts.Common.Helpers
{
    public static class ValueRequiredExtension
    {
        // String validations
        public static string Require(this string requiredString, string parameterName = "(RequiredString)")
        {
            if (requiredString == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            if (requiredString.Length == 0)
            {
                throw new ArgumentOutOfRangeException(parameterName, "Value must not be empty.");
            }

            if (requiredString.Trim().Length == 0)
            {
                throw new ArgumentOutOfRangeException(parameterName, "Value must contain more than white space.");
            }

            return requiredString;
        }

        // Guid validations
        public static Guid Require(this Guid requiredGuid, string parameterName = "(RequiredGuid)")
        {
            if (requiredGuid == Guid.Empty)
            {
                throw new ArgumentException("Guid must not be empty (default)", parameterName);
            }

            return requiredGuid;
        }
    }
}
