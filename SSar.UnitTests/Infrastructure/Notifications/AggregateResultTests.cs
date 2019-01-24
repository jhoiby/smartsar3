using System;
using Shouldly;
using SSar.Contexts.Common.Domain.Entities;
using SSar.Contexts.Common.Domain.Notifications;
using Xunit;

namespace SSar.UnitTests.Infrastructure.Notifications
{
    public class AggregateResultTests
    {
        private class TestAggregate : AggregateRoot
        {
        }

        [Fact]
        public void FromAggregate_populates_AggregateResult()
        {
            var agg = new TestAggregate();

            var result = AggregateResult<TestAggregate>.FromAggregate(agg);

            result.ShouldSatisfyAllConditions(
                () => result.ShouldBeOfType<AggregateResult<TestAggregate>>(),
                () => result.Aggregate.ShouldBeSameAs(agg),
                () => result.Notifications.Count.ShouldBe(0));
        }

        [Fact]
        public void FromAggregate_throws_if_given_null()
        {
            var ex = Should.Throw<ArgumentNullException>(() => AggregateResult<TestAggregate>.FromAggregate(null));

            ex.ParamName.ShouldBe("aggregate");
        }

        [Fact]
        public void FromNotifications_populates_AggregateResult()
        {
            var notificationList = new NotificationList("param", "message");

            var result = AggregateResult<TestAggregate>.FromNotifications(notificationList);

            result.ShouldSatisfyAllConditions(
                    () => result.Notifications.ShouldBe(notificationList),
                    () => result.Aggregate.ShouldBeNull()
                );
        }

        [Fact]
        public void FromNotifications_throws_if_given_null()
        {
            var ex = Should.Throw<ArgumentNullException>(() => AggregateResult<TestAggregate>.FromNotifications(null));

            ex.ParamName.ShouldBe("notifications");
        }

        [Fact]
        public void FromAggregateOrNotifications_given_empty_notifications_returns_aggregate()
        {
            var testAggregate = new TestAggregate();
            var notifications = new NotificationList();

            var result = AggregateResult<TestAggregate>
                .FromAggregateOrNotifications(testAggregate, notifications);

            result.ShouldSatisfyAllConditions(
                () => result.Aggregate.ShouldBe(testAggregate),
                () => result.Notifications.Count.ShouldBe(0));
        }

        [Fact]
        public void FromAggregateOrNotifications_given_populated_notifications_returns_notifications()
        {
            var testAggregate = new TestAggregate();
            var notifications = new NotificationList("param1", "message1");

            var result = AggregateResult<TestAggregate>
                .FromAggregateOrNotifications(testAggregate, notifications);

            result.ShouldSatisfyAllConditions(
                () => result.Aggregate.ShouldBe(null),
                () => result.Notifications.Count.ShouldBe(1));


        }
    }
}
