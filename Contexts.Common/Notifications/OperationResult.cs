using System;
using System.Collections.Generic;
using System.Text;

namespace SSar.Contexts.Common.Results
{
    public class OperationResult
    {
        private ErrorDictionary _errors;

        private OperationResult()
        {
            _errors = new ErrorDictionary();
        }

        public ErrorDictionary Errors => _errors;
        public bool Successful => _errors.Count == 0;

        public static OperationResult CreateEmpty()
        {
            return new OperationResult();
        }

        public static OperationResult CreateFromErrors(ErrorDictionary errors)
        {
            var result = new OperationResult();
            result._errors = errors;

            return result;
        }
    }
}
