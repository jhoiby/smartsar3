using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shouldly;
using SSar.Contexts.Common.Notifications;
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
        }
    }
}
