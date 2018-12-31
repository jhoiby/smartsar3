using SSar.Contexts.Common.Results;

namespace SSar.Contexts.Common.AbstractClasses
{
    public abstract class AggregateRoot : Entity
    {
        private ErrorDictionary _errors;

        protected AggregateRoot()
        {
            _errors = new ErrorDictionary();
        }

        protected ErrorDictionary Errors => _errors;

        protected bool HasErrors => _errors.Count > 0;

        protected void AddError(string key, string value)
        {
            // If key already exists, appends the value to the existing value
            // NOTE: It is recommended error messages be given as full sentences followed
            // by a period so they look proper when several are appended into one string.
            _errors.AddOrAppend(key, value);
        }

    }
}
