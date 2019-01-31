using System;
using Shouldly;
using SSar.Contexts.Common.Domain.Notifications;
using Xunit;

namespace SSar.UnitTests.Contexts.Common.Domain.Notifications
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
