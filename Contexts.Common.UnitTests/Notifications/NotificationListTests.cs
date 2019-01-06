using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shouldly;
using SSar.Contexts.Common.AbstractClasses;
using SSar.Contexts.Common.Notifications;
using SSar.Contexts.Membership.Domain.Entities;
using Xunit;

namespace SSar.Contexts.Common.UnitTests.Notifications
{
    public class NotificationListTests
    {
        [Fact]
        public void AddNotification_adds_complete_notification()
        {
            var notificationList = new NotificationList();

            notificationList.AddNotification("message1", "key1");

            notificationList.ShouldSatisfyAllConditions(
                () => notificationList.First().Message.ShouldBe("message1"),
                () => notificationList.First().ForField.ShouldBe("key1"));
        }

        [Fact]
        public void AddNotification_returns_NotificationList()
        {
            var notificationList = new NotificationList();

            var list2 = notificationList.AddNotification("message1", "key1");

            list2.ShouldSatisfyAllConditions(
                () => list2.Count.ShouldBe(1),
                () => list2.ShouldBeOfType<NotificationList>());
        }

        [Fact]
        public void Empty_returns_true_if_no_list_elements()
        {
            var notificationList = new NotificationList();

            notificationList.Empty.ShouldBe(true);
        }

        [Fact]
        public void Empty_returns_false_if_has_list_elements()
        {
            var notificationList = new NotificationList();

            notificationList.AddNotification("message1", "key1");

            notificationList.Empty.ShouldBe(false);
        }

        private class TestAggregate : AggregateRoot
        {
        }

        [Fact]
        public void AddFromResult_add_notifications_from_INotificationResult()
        {
            var sourceNotificationList = new NotificationList();
            sourceNotificationList.AddNotification("Source message 1.", "Source");
            sourceNotificationList.AddNotification("Source message 2", "Source");

            AggregateResult<TestAggregate> aggResult =
                AggregateResult<TestAggregate>.FromNotificationList(sourceNotificationList);

            var targetList = new NotificationList();
            targetList.AddFromResult(aggResult);

            targetList.Count.ShouldBe(2);
        }
    }
}
