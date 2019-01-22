using System;
using SSar.Infrastructure.Notifications;

namespace SSar.Infrastructure.Entities
{
    public class AggregateResult<TAggregate> where TAggregate : IAggregateRoot
    {
        private readonly TAggregate _aggregate;
        private readonly NotificationList _notifications = new NotificationList();

        private AggregateResult()
        {
        }

        private AggregateResult(TAggregate aggregate)
        {
            _aggregate = aggregate;
        }

        private AggregateResult(NotificationList notifications)
        {
           _notifications = notifications;
        }

        public TAggregate Aggregate => _aggregate;
        public NotificationList Notifications => _notifications;

        public static AggregateResult<TAggregate> FromAggregate(TAggregate aggregate)
        {
            if (aggregate == null)
            {
                throw new ArgumentNullException(nameof(aggregate));
            }

            return new AggregateResult<TAggregate>(aggregate);
        }

        public static AggregateResult<TAggregate> FromNotifications(NotificationList notifications)
        {
            if (notifications == null)
            {
                throw new ArgumentNullException(nameof(notifications));
            }

            return new AggregateResult<TAggregate>(notifications);
        }

        public static AggregateResult<TAggregate> FromAggregateOrNotifications(
            TAggregate aggregate, NotificationList notifications)
        {
            return 
                notifications.Count > 0
                    ? AggregateResult<TAggregate>.FromNotifications(notifications)
                    : AggregateResult<TAggregate>.FromAggregate(aggregate);
        }
    }
}
