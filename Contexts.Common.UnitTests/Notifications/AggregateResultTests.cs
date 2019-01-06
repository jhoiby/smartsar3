﻿using System;
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
        public void Successful_sets_status_to_successful()
        {
            var result = AggregateResult<TestAggregate>.Successful();
            result.Status.ShouldBe(ResultStatus.Successful);
        }

        [Fact]
        public void Successful_sets_Aggregate_property()
        {
            var agg = new TestAggregate();

            var result = AggregateResult<TestAggregate>.Successful(agg);

            result.Aggregate.ShouldBe(agg);
        }

        [Fact]
        public void FromMessage_adds_notification_with_message()
        {
            var result = AggregateResult<TestAggregate>.FromMessage("message1", "field1");

            result.Notifications.First().Message.ShouldBe("message1");
            result.Notifications.First().ForField.ShouldBe("field1");
            result.Status.ShouldBe(ResultStatus.HasNotifications);
        }

        [Fact]
        public void FromException_returns_exception_notification_and_status()
        {
            var exception = new ArgumentNullException("You screwed up.");

            var result = AggregateResult<TestAggregate>.FromException(exception, "The programmer screwed up.");

            result.ShouldSatisfyAllConditions(
                () => result.Notifications.First().Message.ShouldBe("The programmer screwed up."),
                () => result.Notifications.First().ForField.ShouldBe("ExceptionInfo"),
                () => result.Exception.ShouldBe(exception)
                );
        }

        [Fact]
        public void FromException_returns_no_notifications_if_no_message()
        {
            var exception = new ArgumentNullException("You screwed up.");

            var result = AggregateResult<TestAggregate>.FromException(exception);

            result.Notifications.ShouldBeEmpty();
        }

        [Fact]
        public void FromNotificationList_returns_notifications_and_status()
        {
            var notifications = new NotificationList();
            notifications.Add(new Notification("message1", "key1"));

            var result = AggregateResult<TestAggregate>.FromNotificationList(notifications);

            result.ShouldSatisfyAllConditions(
                () => result.Notifications.Count.ShouldBe(1),
                () => result.Status.ShouldBe(ResultStatus.HasNotifications)
                );
        }
    }
}