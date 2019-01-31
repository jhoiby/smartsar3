using SSar.Contexts.Common.Domain.AggregateRoots;
using SSar.Contexts.Common.Domain.Entities;

namespace SSar.Contexts.Common.Domain.Notifications
{
    public static class AggregateResultExtensions
    {
        public static NotificationList AddNotificationsTo<TAggregate>(
            this AggregateResult<TAggregate> sourceResult, NotificationList targetList)
            where TAggregate : IAggregateRoot
        {
            return targetList.AddNotifications(sourceResult.Notifications);
        }
    }
}
