using SSar.Contexts.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using Xunit;

namespace SSar.Contexts.Common.UnitTests.Results
{
    public class ErrorDictionaryTests
    {
        [Fact]
        public void Inherits_from_DictionaryStringString()
        {
            var errorDictionary = new ErrorDictionary();

            errorDictionary.ShouldBeAssignableTo(typeof(Dictionary<string, string>));
        }

        [Fact]
        public void AddOrAppend_adds_KeyValuePair_to_dictionary()
        {
            var errorDictionary = new ErrorDictionary();

            errorDictionary.AddOrAppend("key1", "value1");

            errorDictionary.ShouldContainKey("key1");
            errorDictionary["key1"].ShouldBe("value1");
        }

        [Fact]
        public void AddOrAppend_appends_value_if_key_exists()
        {
            var errorDictionary = new ErrorDictionary();

            errorDictionary.AddOrAppend("key1", "value1");
            errorDictionary.AddOrAppend("key1", "value2");

            errorDictionary["key1"].ShouldBe("value1 value2");
        }

        [Fact]
        public void AddOrAppend_adds_new_key_without_affecting_existing_key()
        {
            var errorDictionary = new ErrorDictionary();

            errorDictionary.AddOrAppend("key1", "value1");
            errorDictionary.AddOrAppend("key2", "value2");

            errorDictionary["key1"].ShouldBe("value1");
        }
    }
}
