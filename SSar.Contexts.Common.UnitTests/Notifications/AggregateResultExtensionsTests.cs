﻿using System;
using System.Linq;
using Shouldly;
using SSar.Infrastructure.Entities;
using SSar.Infrastructure.Notifications;
using Xunit;

namespace SSar.Contexts.Common.UnitTests.Notifications
{
    public class AggregateResultExtensionsTests
    {
        private class TestAggregate : AggregateRoot
        {
        }

        [Fact]
        public void AddNotificationsTo_updates_target_list()
        {

            var aggregateNotificationList = new NotificationList()
                .AddNotification("param1", "message1 for param1")
                .AddNotification("param1", "message2 for param1")
                .AddNotification("param2", "message1 for param2")
                .AddNotification("param3", "message1 for param3");

            var targetNotificationList = new NotificationList()
                .AddNotification("param1", "message3 for param1")
                .AddNotification("param1", "message4 for param1")
                .AddNotification("param2", "message2 for param2")
                .AddNotification("param4", "message1 for param4");

            var aggregate = AggregateResult<TestAggregate>.FromNotifications(aggregateNotificationList);

            aggregate.AddNotificationsTo(targetNotificationList);

            targetNotificationList.ShouldSatisfyAllConditions(
                () => targetNotificationList["param1"].Count.ShouldBe(4),
                () => targetNotificationList["param2"].Count.ShouldBe(2),
                () => targetNotificationList["param3"].Count.ShouldBe(1),
                () => targetNotificationList["param4"].Count.ShouldBe(1),
                () => targetNotificationList["param1"].Any(n => n.Message == "message2 for param1").ShouldBeTrue(),
                () => targetNotificationList["param1"].Any(n => n.Message == "message4 for param1").ShouldBeTrue());
        }
    }
}
