using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using SSar.Contexts.Common.Application.Commands;
using SSar.Contexts.Common.Domain.AggregateRoots;
using SSar.Contexts.Common.Domain.Entities;
using SSar.Contexts.Common.Domain.Notifications;
using Xunit;

namespace SSar.UnitTests.Contexts.Common.Application.Commands
{
    public class AggregateResultsExtenstionsTests
    {
        private class ConcreteAggregate : AggregateRoot
        {
        }

        [Fact]
        public void Case_succeeded_should_set_properties()
        {
            var aggregate = new ConcreteAggregate();
            var aggregateResult = AggregateResult<ConcreteAggregate>.Success(aggregate);

            var commandResult = aggregateResult.ToCommandResult();

            commandResult.ShouldSatisfyAllConditions(
                () => commandResult.Succeeded.ShouldBeTrue(),
                () => commandResult.Notifications.ShouldBeEmpty(),
                () => commandResult.Exception.ShouldBeNull(),
                () => commandResult.Status.ShouldBe(CommandResultStatus.Succeeded)
                );
        }

        [Fact]
        public void Case_fail_without_exception_should_set_properties()
        {

            var aggregate = new ConcreteAggregate();
            var notifications = new NotificationList("Anvil", "Must not land on Road Runner");
            var aggregateResult = AggregateResult<ConcreteAggregate>.Fail(notifications);

            var commandResult = aggregateResult.ToCommandResult();

            commandResult.ShouldSatisfyAllConditions(
                () => commandResult.Succeeded.ShouldBeFalse(),
                () => commandResult.Notifications["Anvil"]
                    .ShouldContain(n => n.Message.Contains("Must not land on Road Runner"), 1),
                () => commandResult.Exception.ShouldBeNull(),
                () => commandResult.Status.ShouldBe(CommandResultStatus.ValidationError)
            );
        }

        [Fact]
        public void Case_fail_with_exception_should_set_properties()
        {

            var aggregate = new ConcreteAggregate();
            var notifications = new NotificationList("Anvil", "Must not land on Road Runner");
            var exception = new Exception("Anvil landed on Road Runner.");
            var aggregateResult = AggregateResult<ConcreteAggregate>.Fail(notifications, exception);

            var commandResult = aggregateResult.ToCommandResult();

            commandResult.ShouldSatisfyAllConditions(
                () => commandResult.Succeeded.ShouldBeFalse(),
                () => commandResult.Notifications["Anvil"]
                    .ShouldContain(n => n.Message.Contains("Must not land on Road Runner"), 1),
                () => commandResult.Exception.ShouldBe(exception),
                () => commandResult.Status.ShouldBe(CommandResultStatus.Exception)
            );
        }
    }
}
