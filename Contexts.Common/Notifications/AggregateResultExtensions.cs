using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SSar.Contexts.Common.AbstractClasses;

namespace SSar.Contexts.Common.Notifications
{
    public static class AggregateResultExtensions
    {
        public static NotificationList CopyNotificationsTo<TAggregate>(
            this AggregateResult<TAggregate> aggregateResult, NotificationList targetList) 
            where TAggregate : IAggregateRoot
        {
            targetList.AddRange(aggregateResult.Notifications);

            return targetList;
        }

        public static CommandResult ToCommandResult<TAggregate>(
            this AggregateResult<TAggregate> aggregateResult) 
            where TAggregate : IAggregateRoot
        {
            var aggregateId = aggregateResult == null
                ? default(Guid)
                : aggregateResult.Aggregate.Id;

            var status = aggregateResult.Notifications.Count == 0
                ? ResultStatus.Successful
                : ResultStatus.HasNotifications;

            status = aggregateResult.Exception == null
                ? status
                : ResultStatus.HasException; // HasException implies HasNotifications

            return new CommandResult
            (status, aggregateResult.Notifications,
                aggregateResult.Exception, aggregateId);
        }
    }
}
