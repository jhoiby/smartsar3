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
    }
}
