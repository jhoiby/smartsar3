using System;
using SSar.Contexts.Common.Results;

namespace SSar.Contexts.Common.AbstractClasses
{
    // TODO: Reconsider these methods
    // This seems to have a bit of a code smell (violation of SRP) by having the following three
    // items here: 
    //    1. Error tracking
    //    2. Validation
    //    3. Result building
    // On the other hand, a primary responsibility of an Aggregate Root is to act as
    // a consistency boundary, and these methods provided the ability to enforce that.
    // I need to think about it for a while.


    public abstract class AggregateRoot : Entity
    {
        private ErrorDictionary _errors;

        protected AggregateRoot()
        {
            _errors = new ErrorDictionary();
        }

        protected ErrorDictionary Errors => _errors;

        protected bool HasErrors => _errors.Count > 0;

        private void ClearErrors()
        {
            _errors = new ErrorDictionary();
        }

        protected void AddError(string key, string value)
        {
            // If key already exists, appends the value to the existing value
            // NOTE: It is recommended error messages be given as full sentences followed
            // by a period so they look proper if several are appended into one string.
            _errors.AddOrAppend(key, value);
        }

        protected void ValidateDomainRule(Func<bool> rule, string errorKey, string errorMessage)
        {
            if (! rule.Invoke())
            {
                AddError(errorKey, errorMessage);
            }
        }

        protected void ThrowIfNullParam(object param, string paramName)
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        protected OperationResult Result()
        {
            var result = OperationResult.CreateFromErrors(_errors);

            ClearErrors();

            return result;
        }
    }
}
