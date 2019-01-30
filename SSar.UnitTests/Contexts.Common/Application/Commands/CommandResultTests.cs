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
                () => result.Status.ShouldBe(CommandResult.StatusCode.Succeeded));
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
            var result = CommandResult.Failed(CommandResult.StatusCode.DomainValidationError, null);

            result.Notifications.ShouldSatisfyAllConditions(
                () => result.Notifications.ShouldNotBeNull(),
                () => result.Notifications.ShouldBeEmpty());
        }

        [Fact]
        public void Failed_should_set_properties()
        {
            var notificationList = new NotificationList("key1", "Hello world");
            var exception = new Exception("Oopsie!");

            var result = CommandResult.Failed(
                CommandResult.StatusCode.DomainValidationError, notificationList, exception);

            result.ShouldSatisfyAllConditions(
                () => result.Succeeded.ShouldBeFalse(),
                () => result.Status.ShouldBe(CommandResult.StatusCode.DomainValidationError),
                () => result.Notifications.ShouldBe(notificationList),
                () => result.Exception.ShouldBe(exception));
        }

        [Fact]
        public void Failed_given_no_exception_should_return_null_exception()
        {

            var notificationList = new NotificationList("key1", "Hello world");

            var result = CommandResult.Failed(
                CommandResult.StatusCode.DomainValidationError, notificationList);

            result.Exception.ShouldBeNull();
        }

        [Fact]
        public void Failed_given_succeeded_status_should_throw_invalid_operation_exception()
        {

            var notificationList = new NotificationList("key1", "Hello world");

            Should.Throw<InvalidOperationException>( () => 
                CommandResult.Failed(
                    CommandResult.StatusCode.Succeeded, notificationList));
        }
    }
}
