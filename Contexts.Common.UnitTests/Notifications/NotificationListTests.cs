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

            list.AddNotificationForField("message1", "field1");

            list.Notifications.ShouldContain(n => n.ForField == "field1");
        }
    }
}
