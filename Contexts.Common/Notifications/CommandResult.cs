using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SSar.Contexts.Common.AbstractClasses;

namespace SSar.Contexts.Common.Notifications
{
    public class CommandResult : INotificationResult
    {
        public CommandResult(
            ResultStatus status, 
            NotificationList notifications, 
            Exception exception, 
            Guid aggregateId = default(Guid))
        {
            Status = status;
            Notifications = notifications ?? new NotificationList();
            Exception = exception; // Null allowed
            AggregateId = aggregateId;
        }

        public ResultStatus Status { get; }
        public NotificationList Notifications { get; }
        public Exception Exception { get; }

        // TODO: Consider returning List<Guid> to handle case where multiple aggregates operated on by one command
        public Guid AggregateId { get; }
        public bool HasNotifications => Notifications.Count > 0;


        //// TODO: SEPARATE THIS OUT TO EXTENSION (AggregateResult.ToCommandResult())
        //public static CommandResult FromAggregateResult<TAggregate>(
        //    AggregateResult<TAggregate> aggregateResult)
        //    where TAggregate : IAggregateRoot
        //{
        //    // TODO: RETHINK, REWRITE (IF NEEDED) AND UNIT TEST THIS
        //    // TODO: VERIFY ALL CASES ARE COVERED

        //    var aggregateId = aggregateResult == null
        //        ? default(Guid)
        //        : aggregateResult.Aggregate.Id;

        //    var status = aggregateResult.Notifications.Count == 0
        //        ? ResultStatus.Successful
        //        : ResultStatus.HasNotifications;

        //    status = aggregateResult.Exception == null
        //        ? status
        //        : ResultStatus.HasException; // HasException implies HasNotifications

        //    return new CommandResult
        //        (status, aggregateResult.Notifications,
        //            aggregateResult.Exception, aggregateId);
        //}
    }
}
