using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using SSar.Contexts.Common.Notifications;
using Xunit;

namespace SSar.Contexts.Common.UnitTests.Notifications
{
    public class NotificationTests
    {
        [Fact]
        public void Constructor_sets_message()
        {
            var notification = new Notification("hello");

            notification.Message.ShouldBe("hello");
        }

        [Fact]
        public void Constructor_throws_if_message_null()
        {
            var ex = Should.Throw<ArgumentNullException>(
                () => new Notification(null));

            ex.ParamName.ShouldBe("message");
        }
    }
}
