using System;
using System.Collections.Generic;
using System.Text;

namespace SSar.Contexts.Common.Results
{
    /// <summary>
    /// A collection of errors that occured during handling of a command. The key
    /// contains the name of the parameter, if any, that triggered the error. The value
    /// contains the error message. If the error message is not related to a specific paramater
    /// or other identifiable item specific to the command/query the key may be set to "".
    ///
    /// This types in this class were chosen to work easily with Asp.Net ModelError dictionaries.
    /// </summary>
    public class ErrorDictionary : Dictionary<string, string>
    {
        // Attempts to add pair to the dictionary. If the key already exists, appends the
        // value to the existing value with an optional delimeter between. This is useful when,
        // for example, adding multiple error strings for a given parameter (key).
        public void AddOrAppend(string key, string value)
        {
            if (this.ContainsKey(key))
            {
                string originalValue = this.GetValueOrDefault(key);
                this.Remove(key);
                this.Add(key, originalValue + " " + value);
            }
            else
            {
                this.Add(key, value);
            }
        }
    }
}
