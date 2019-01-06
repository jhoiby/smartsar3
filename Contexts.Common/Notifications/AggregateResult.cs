using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using SSar.Contexts.Common.AbstractClasses;

namespace SSar.Contexts.Common.Notifications
{
    public class AggregateResult<TAggregate> where TAggregate : IAggregateRoot
    {
        private AggregateResult()
        {
        }

        public ResultStatus Status { get; private set; }
        public NotificationList Notifications { get; private set; } // TODO: Return readonly
        public Exception Exception { get; private set; }
        public TAggregate Aggregate { get; private set; }

        public static AggregateResult<TAggregate> Successful(TAggregate aggregate = default(TAggregate))
        {
            var result = new AggregateResult<TAggregate>
            {
                Status = ResultStatus.Successful,
                Aggregate = aggregate, // Null allowed
                Notifications = new NotificationList()
            };

            return result;
        }

        public static AggregateResult<TAggregate> FromMessage(string message, string fieldKey = "")
        {
            var notificationList = new NotificationList
            {
                new Notification(message, fieldKey)
            };

            return new AggregateResult<TAggregate>
            {
                Status = ResultStatus.HasNotifications,
                Notifications = notificationList
            };
        }

        public static AggregateResult<TAggregate> FromNotificationList(NotificationList notificationList)
        {
            return new AggregateResult<TAggregate>
            {
                Status = ResultStatus.HasNotifications,
                Notifications = notificationList
            };
        }

        public static AggregateResult<TAggregate> FromException(Exception exception, string userMessage = "")
        {
            var notificationList = new NotificationList();

            if (userMessage.Length > 0)
            {
                notificationList.Add(new Notification(userMessage, "ExceptionInfo"));
            }

            return new AggregateResult<TAggregate>
            {
                Status = ResultStatus.HasException,
                Notifications = notificationList,
                Exception = exception
            };
        }

        public CommandResult ToCommandResult()
        {
            Guid aggregateId = default(Guid);

            if (Aggregate != null)
            {
                aggregateId = Aggregate.Id;
            }

            return new CommandResult(Status, Notifications, Exception, aggregateId);
        }
    }
}
