using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace SSar.Contexts.Common.Helpers
{
    public static class ThrowExceptionHelpers
    {
        public static void ThrowIfArgumentNull(this object obj, string paramName = null)
        {
            if (obj == null && paramName == null)
            {
                throw new ArgumentNullException();
            }

            if (obj == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
