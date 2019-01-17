using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SSar.Contexts.Common.Entities;

namespace SSar.Contexts.Common.Notifications
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
