using System;
using SSar.Contexts.Common.Domain.Entities;

namespace SSar.Contexts.Common.Domain.Notifications
{
    public class AggregateResult<TAggregate> where TAggregate : IAggregateRoot
    {
        public bool Succeeded;
        public NotificationList Notifications;
        public Exception Exception;
        public TAggregate NewAggregate;

        public static AggregateResult<TAggregate> Success()
        {
            return Success(default(TAggregate));
        }

        public static AggregateResult<TAggregate> Success(TAggregate aggregate)
        {
            return new AggregateResult<TAggregate>
            {
                Succeeded = true,
                Notifications = new NotificationList(),
                Exception = null,
                NewAggregate = aggregate
            };
        }

        public static AggregateResult<TAggregate> Fail(
            NotificationList notifications, Exception exception = null)
        {
            return new AggregateResult<TAggregate>
            {
                Succeeded = false,
                Notifications = notifications ?? new NotificationList(),
                Exception = exception,
                NewAggregate = default(TAggregate)
            };
        }

        public static AggregateResult<TAggregate> Fail(Exception exception)
        {
            return AggregateResult<TAggregate>.Fail(new NotificationList(), exception);
        }
    }
}
