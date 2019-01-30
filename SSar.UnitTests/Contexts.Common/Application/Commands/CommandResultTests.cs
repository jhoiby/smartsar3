using System;
using Shouldly;
using SSar.Contexts.Common.Application.Commands;
using SSar.Contexts.Common.Domain.Notifications;
using Xunit;

namespace SSar.UnitTests.Contexts.Common.Application.Commands
{
    public class CommandResultTests
    {
        [Fact]
        public void Success_should_return_succeeded_result()
        {
            var result = CommandResult.Success;

            result.ShouldSatisfyAllConditions(
                () => result.Succeeded.ShouldBeTrue(),
                () => result.Status.ShouldBe(CommandResultStatus.Succeeded));
        }

        [Fact]
        public void Success_should_return_empty_metadata()
        {
            var result = CommandResult.Success;

            result.Succeeded.ShouldSatisfyAllConditions(
                () => result.Notifications.ShouldBeEmpty(),
                () => result.Exception.ShouldBeNull());
        }

        [Fact]
        public void Failed_given_null_notifications_should_return_empty_list()
        {
            var result = CommandResult.Fail(notifications: null);

            result.Notifications.ShouldSatisfyAllConditions(
                () => result.Notifications.ShouldNotBeNull(),
                () => result.Notifications.ShouldBeEmpty());
        }

        [Fact]
        public void Failed_should_set_properties()
        {
            var notificationList = new NotificationList("key1", "Hello world");
            var exception = new Exception("Oopsie!");

            var result = CommandResult.Fail(notificationList, exception);

            result.ShouldSatisfyAllConditions(
                () => result.Succeeded.ShouldBeFalse(),
                () => result.Status.ShouldBe(CommandResultStatus.Exception),
                () => result.Notifications.ShouldBe(notificationList),
                () => result.Exception.ShouldBe(exception));
        }

        [Fact]
        public void Failed_given_no_exception_should_return_null_exception()
        {

            var notificationList = new NotificationList("key1", "Hello world");

            var result = CommandResult.Fail(notificationList);

            result.Exception.ShouldBeNull();
        }
    }
}
