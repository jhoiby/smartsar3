using SSar.Infrastructure.Notifications;

namespace SSar.Domain.Infrastructure
{
    public static class AggregateResultExtensions
    {
        public static NotificationList AddNotificationsTo<TAggregate>(
            this AggregateResult<TAggregate> sourceResult, NotificationList targetList)
            where TAggregate : IAggregateRoot
        {
            return targetList.AddOrAppend(sourceResult.Notifications);
        }
    }
}
