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
        public void FromNotification_returns__type_NotificationList()
        {
            var message = new Notification("hello1");

            var notificationList = NotificationList.FromNotification("param", message);

            notificationList.ShouldBeOfType<NotificationList>();
        }

        [Fact]
        public void FromNotification_add_message()
        {
            var message = new Notification("hello1");

            var notificationList = NotificationList.FromNotification("param", message);

            notificationList.Notifications["param"].First().ShouldBe(message);
        }
    }
}
