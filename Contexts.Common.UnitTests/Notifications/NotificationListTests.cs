using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using SSar.Contexts.Common.Notifications;
using Xunit;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace SSar.Contexts.Common.UnitTests.Notifications
{
    public class NotificationListTests
    {
        [Fact]
        public void Constructor_initializes_list()
        {
            var list = new NotificationList();

            list.Notifications.ShouldNotBeNull();
        }

        [Fact]
        public void AddNotificationStringString_adds_to_list()
        {
            var list = new NotificationList();

            list.AddNotification("message1", "field1");

            list.Notifications.ShouldContain(n => n.ForField == "field1");
        }

        [Fact]
        public void AddNotifications_adds_to_list_without_deleting()
        {
            var targetList = new NotificationList();
            targetList.AddNotification("message1", "field1");

            var sourceList = new NotificationList();
            sourceList.AddNotification("message2", "field2");
            sourceList.AddNotification("message3", "field3");

            targetList.AddNotifications(sourceList);

            targetList.Notifications.Count.ShouldBe(3);
        }

        [Fact]
        public void ToNotificationList_adds_source_notifications_to_target()
        {
            var sourceList = new NotificationList();
            sourceList.AddNotification("message1", "field1");
            sourceList.AddNotification("message2", "field2");
            sourceList.AddNotification("message3", "field3");

            var targetList = new NotificationList();

            sourceList.ToNotificationList(targetList);

            targetList.Notifications.Count.ShouldBe(3);
        }
    }
}
