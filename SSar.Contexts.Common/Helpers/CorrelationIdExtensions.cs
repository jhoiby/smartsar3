using System;
using System.Collections.Generic;
using System.Text;

namespace SSar.Contexts.Common.Helpers
{
    public static class CorrelationIdExtensions
    {
        public static string ToCorrelationId(this Guid id)
        {
            return id.ToString().Substring(0, 8);
        }
    }
}
