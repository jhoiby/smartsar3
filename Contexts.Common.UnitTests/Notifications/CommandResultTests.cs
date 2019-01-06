using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using SSar.Contexts.Common.Notifications;
using Xunit;

namespace SSar.Contexts.Common.UnitTests.Notifications
{
    public class CommandResultTests
    {
        [Fact]
        public void Constructor_sets_properties()
        {
            var cmdResult = new CommandResult(
                ResultStatus.HasException, 
                new NotificationList(), 
                new ArgumentNullException(), 
                Guid.NewGuid());

            cmdResult.ShouldSatisfyAllConditions(
                () => cmdResult.Status.ShouldBe(ResultStatus.HasException),
                () => cmdResult.Notifications.ShouldNotBeNull(),
                () => cmdResult.Exception.ShouldBeOfType<ArgumentNullException>(),
                () => cmdResult.AggregateId.ShouldNotBe(default(Guid)));
        }

        [Fact]
        public void Creates_new_notification_list_if_param_is_null()
        {
            var cmdResult = new CommandResult(
                ResultStatus.HasException,
                null,
                null,
                Guid.NewGuid());

            cmdResult.Notifications.ShouldNotBeNull();
        }
    }
}
