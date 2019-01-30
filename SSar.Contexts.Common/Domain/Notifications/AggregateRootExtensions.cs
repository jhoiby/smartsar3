using System;
using System.Collections.Generic;
using System.Text;
using Remotion.Linq.Clauses;
using SSar.Contexts.Common.Domain.Entities;

namespace SSar.Contexts.Common.Domain.Notifications
{
    // TODO: UNIT TEST ALL METHODS

    public static class AggregateRootExtensions
    {
        public struct AggregateAndNotifications<TAggregate> where TAggregate : IAggregateRoot
        {
            public TAggregate Aggregate;
            public NotificationList Notifications;

        }

        public static AggregateAndNotifications<TAggregate> OrNotifications<TAggregate>(this TAggregate aggregate, NotificationList notifications)
            where TAggregate : IAggregateRoot
        {
            return new AggregateAndNotifications<TAggregate>
            {
                Aggregate = aggregate,
                Notifications = notifications
            };
        }

        public static AggregateResult<TAggregate> AsResult<TAggregate>(this AggregateAndNotifications<TAggregate> info)
            where TAggregate : IAggregateRoot
        {
            return info.Notifications.HasNotifications
                ? AggregateResult<TAggregate>.Fail(info.Notifications)
                : AggregateResult<TAggregate>.Success(info.Aggregate);
        }
    }
}
