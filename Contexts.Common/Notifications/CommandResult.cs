using System;
using System.Collections.Generic;
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


        // TODO: Learn why I can't take an IAggregateRoot here instead of a generic!
        // I ended up creating the AggregateResult.ToCommandResult() method instead

        //public static CommandResult FromAggregateResult(AggregateResult<IAggregateRoot> aggregateResult)
        //{
        //    Guid aggregateId = default(Guid);

        //    if (aggregateResult != null)
        //    {
        //        aggregateId = aggregateResult.Aggregate.Id;
        //    }

        //    return new CommandResult
        //        (aggregateResult.Status, aggregateResult.Notifications, 
        //            aggregateResult.Exception, aggregateId);
        //}
    }
}
