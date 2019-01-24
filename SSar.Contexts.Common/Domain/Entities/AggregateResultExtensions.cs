using SSar.Contexts.Common.Domain.Notifications;

namespace SSar.Contexts.Common.Domain.Entities
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
