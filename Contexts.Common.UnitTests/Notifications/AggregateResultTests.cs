using System;
using System.Linq;
using Shouldly;
using SSar.Contexts.Common.AbstractClasses;
using SSar.Contexts.Common.Notifications;
using Xunit;

namespace SSar.Contexts.Common.UnitTests.Notifications
{
    public class AggregateResultTests
    {
        private class TestAggregate : AggregateRoot
        {
        }

        [Fact]
        public void WithAggregate_returns_AggregateResult_with_aggregate()
        {
            var agg = new TestAggregate();

            var result = AggregateResult<TestAggregate>.FromAggregate(agg);

            result.ShouldSatisfyAllConditions(
                () => result.ShouldBeOfType<AggregateResult<TestAggregate>>(),
                () => result.Aggregate.ShouldBeSameAs(agg));
        }
    }
}
