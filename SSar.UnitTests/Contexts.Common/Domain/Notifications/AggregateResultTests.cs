using SSar.Contexts.Common.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Shouldly;
using SSar.Contexts.Common.Domain.Entities;
using Xunit;

namespace SSar.UnitTests.Contexts.Common.Domain.Notifications
{
    public class AggregateResultTests
    {
        private class ConcreteAggregate : AggregateRoot
        {
        }

        [Fact]
        public void Success_should_return_correct_properties()
        {
            var result = AggregateResult<ConcreteAggregate>.Success();

            result.ShouldSatisfyAllConditions(
                () => result.Succeeded.ShouldBeTrue(),
                () => result.Notifications.ShouldBeEmpty(),
                () => result.Exception.ShouldBeNull(),
                () => result.NewAggregate.ShouldBe(default(ConcreteAggregate)));
        }

        [Fact]
        public void Success_with_aggregate_should_return_aggregate()
        {
            var aggregate = new ConcreteAggregate();

            var result = AggregateResult<ConcreteAggregate>.Success(aggregate);

            result.ShouldSatisfyAllConditions(
                () => result.Succeeded.ShouldBeTrue(),
                () => result.Notifications.ShouldBeEmpty(),
                () => result.Exception.ShouldBeNull(),
                () => result.NewAggregate.ShouldBe(aggregate));
        }

        [Fact]
        public void Fail_should_return_correct_properties()
        {
            var notifications = new NotificationList("key1", "notification1");
            var exception = new Exception("Oopsie!");

            var result = AggregateResult<ConcreteAggregate>.Fail(notifications, exception);

            result.ShouldSatisfyAllConditions(
                () => result.Succeeded.ShouldBeFalse(),
                () => result.Notifications.ShouldBe(notifications),
                () => result.Exception.ShouldBe(exception),
                () => result.NewAggregate.ShouldBeNull());
        }

        [Fact]
        public void Fail_given_null_notifications_should_return_empty_notifcations()
        {
            var result = AggregateResult<ConcreteAggregate>.Fail(null);

            result.Notifications.ShouldBeEmpty();
        }

        [Fact]
        public void Fail_given_exception_only_should_set_properties()
        {
            var exception = new Exception("ой");

            var result = AggregateResult<ConcreteAggregate>.Fail(exception);

            result.Exception.ShouldBe(exception);
        }
    }
}
