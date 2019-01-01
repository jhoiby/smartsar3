using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using SSar.Contexts.Common.Results;
using Xunit;

namespace SSar.Contexts.Common.UnitTests.Results
{
    public class MergeResultExtensionTests
    {
        [Fact]
        public void Original_result_errors_merged_to_target_result()
        {
            var originalResult = OperationResult.CreateEmpty();
            var targetResult = OperationResult.CreateEmpty();

            originalResult.Errors.AddOrAppend("key1", "value1");
            originalResult.Errors.AddOrAppend("key2", "value2");

            originalResult.AppendErrorsTo(targetResult);

            targetResult.ShouldSatisfyAllConditions(
                () => targetResult.Errors["key1"].ShouldBe("value1"),
                () => targetResult.Errors["key2"].ShouldBe("value2")
                );
        }
    }
}
