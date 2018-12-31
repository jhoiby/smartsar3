using SSar.Contexts.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using Xunit;

namespace SSar.Contexts.Common.UnitTests.Results
{

    public class OperationResultTests
    {
        [Fact]
        public void CreateSuccessful_should_contain_nonNull_errorDictionary()
        {
            var operationResult = OperationResult.CreateSuccessful();

            operationResult.Errors.ShouldNotBeNull();
        }

        [Fact]
        public void If_no_errors_should_return_successful_true()
        {
            var operationResult = OperationResult.CreateSuccessful();

            operationResult.Successful.ShouldBeTrue();
        }

        [Fact]
        public void If_has_error_should_return_successful_false()
        {
            var errorDictionary = new ErrorDictionary();
            errorDictionary.Add("error key 1", "error message 1");

            var operationResult = OperationResult.CreateFromErrors(errorDictionary);

            operationResult.Successful.ShouldBeFalse();
        }

        [Fact]
        public void Should_contain_populated_error_list()
        {
            var errorDictionary = new ErrorDictionary();
            errorDictionary.Add("error key 1", "error message 1");

            var operationResult = OperationResult.CreateFromErrors(errorDictionary);

            operationResult.Errors["error key 1"].ShouldBe("error message 1");

        }
    }
}
