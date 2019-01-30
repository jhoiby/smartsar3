using System;
using System.Collections.Generic;
using System.Text;
using SSar.Contexts.Common.Domain.Entities;

namespace SSar.Contexts.Common.Domain.Notifications
{
    public static class AggregateRootExtensions
    {
        public struct AggregateAndNotifications
        {
            public IAggregateRoot Aggregate;
            public NotificationList Notifications;

        }

        public static AggregateAndNotifications OrNotifications<TAggregate>(this TAggregate aggregate, NotificationList notifications)
            where TAggregate : IAggregateRoot
        {
            return new AggregateAndNotifications
            {
                Aggregate = aggregate,
                Notifications = notifications
            };
        }

        public static AggregateResult<TAggregate> AsResult<TAggregate>(this AggregateAndNotifications info)
            where TAggregate : IAggregateRoot
        {
            return new AggregateResult<TAggregate>();
        }
    }
}
