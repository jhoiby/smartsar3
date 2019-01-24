using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;
using SSar.Contexts.Common.Domain.Notifications;
using Xunit;

namespace SSar.UnitTests.Infrastructure.Notifications
{
    public class NotificationListTests
    {
        [Fact]
        public void Construct_from_Notification_adds_notification()
        {
            var notification = new Notification("hello1");

            var notificationList = new NotificationList("param", notification);

            notificationList.ShouldSatisfyAllConditions(
                    () => notificationList.Count.ShouldBe(1),
                    () => notificationList["param"].First().ShouldBe(notification)
                );
        }

        [Fact]
        public void Construct_from_message_string_populates_object()
        {
            var notificationList = new NotificationList("param1", "You screwed up.");

            notificationList.ShouldSatisfyAllConditions(
                () => notificationList.Count.ShouldBe(1),
                () => notificationList["param1"].First().Message.ShouldBe("You screwed up.")
                );
        }

        [Fact]
        public void Construct_from_Notification_throws_if_notification_null()
        {
            Notification nullNotification = null;

            var ex = Should.Throw<ArgumentNullException>(() => new NotificationList("param", nullNotification));

            ex.ParamName.ShouldBe("paramNotification");
        }

        [Fact]
        public void Construct_from_NotificationArray_adds_array()
        {
            List<Notification> notifications = new List<Notification>
            {
                new Notification("message1"),
                new Notification("message2")
            };

            var notificationList = new NotificationList("param", notifications);

            notificationList["param"].ShouldBeSameAs(notifications);
        }

        [Fact]
        public void Construct_from_NotificationArray_throws_if_null_notifications()
        {
            List<Notification> notifications = null;

            var ex = Should.Throw<ArgumentNullException>(
                () => new NotificationList("param", notifications));

            ex.ParamName.ShouldBe("paramNotifications");
        }

        [Fact]
        public void Empty_constructor_returns_list_without_notifications()
        {
            var notificationList = new NotificationList();

            notificationList.ShouldBeEmpty();
        }

        [Fact]
        public void HasNotifications_false_if_no_notifications()
        {
            var notificationList = new NotificationList();

            notificationList.HasNotifications.ShouldBeFalse();
        }

        [Fact]
        public void HasNotifications_true_if_notifications()
        {
            var notification = new Notification("hello1");

            var notificationList = new NotificationList("param", notification);

            notificationList.HasNotifications.ShouldBeTrue();
        }

        [Fact]
        public void AddWithAppend_NotificationList_merges_lists()
        {
            var sourceNotificationList = new NotificationList()
                .AddNotification("param1", "message1 for param1")
                .AddNotification("param1", "message2 for param1")
                .AddNotification("param2", "message1 for param2")
                .AddNotification("param3", "message1 for param3");

            var targetNotificationList = new NotificationList()
                .AddNotification("param1", "message3 for param1")
                .AddNotification("param1", "message4 for param1")
                .AddNotification("param2", "message2 for param2")
                .AddNotification("param4", "message1 for param4");

            targetNotificationList.AddOrAppend(sourceNotificationList);

            targetNotificationList.ShouldSatisfyAllConditions(
                () => targetNotificationList["param1"].Count.ShouldBe(4),
                () => targetNotificationList["param2"].Count.ShouldBe(2),
                () => targetNotificationList["param3"].Count.ShouldBe(1),
                () => targetNotificationList["param4"].Count.ShouldBe(1),
                () => targetNotificationList["param1"].Any(n => n.Message == "message2 for param1").ShouldBeTrue(),
                () => targetNotificationList["param1"].Any(n => n.Message == "message4 for param1").ShouldBeTrue());
        }
    }
}
