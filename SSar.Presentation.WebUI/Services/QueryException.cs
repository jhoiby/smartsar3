using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSar.Presentation.WebUI.Services
{
    public class QueryException : Exception
    {
        public QueryException()
        {
        }

        public QueryException(string message) : base(message)
        {
        }

        public QueryException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
