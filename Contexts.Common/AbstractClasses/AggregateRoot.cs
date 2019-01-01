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

        /// <summary>
        /// Creates a new, empty aggregate root with an initialized ErrorDictionary.
        /// Accessible only to internal factory methods as an empty aggregate may
        /// not be permitted by domain rules.
        /// </summary>
        protected AggregateRoot()
        {
            _errors = new ErrorDictionary();
        }

        /// <summary>
        /// Dictionary of errors resulting from current operations on the aggregate.
        /// </summary>
        protected ErrorDictionary Errors => _errors;

        protected bool HasErrors => _errors.Count > 0;


        protected void ClearErrors()
        {
            _errors = new ErrorDictionary();
        }

        /// <summary>
        /// If the error key already exists, appends the error message to the existing
        /// error message.
        /// </summary>
        /// <param name="key">Name of property to which the error applies. Example: "FirstName".</param>
        /// <param name="value">Error message text. Error messages should be created as full sentences
        /// followed by a period so they look correct if several are appended into
        /// one message. Example: "A first name is required."</param>
        protected void AddError(string key, string value)
        {
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

        /// <summary>
        /// Builds an OperationResult containing the error dictionary of the Aggregate, then
        /// clears the Aggregate's errors. It is intended to be used at the end of a command
        /// method.
        /// </summary>
        /// <returns>OperationResult</returns>
        protected OperationResult Result()
        {
            var result = OperationResult.CreateFromErrors(_errors);

            ClearErrors();

            return result;
        }
    }
}
