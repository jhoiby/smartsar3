using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SSar.Contexts.Common.AbstractClasses;

namespace SSar.Contexts.Common.Notifications
{
    public static class AggregateResultExtensions
    {
        public static NotificationList AddNotificationsTo<TAggregate>(
            this AggregateResult<TAggregate> aggregateResult, NotificationList targetList) 
            where TAggregate : IAggregateRoot
        {
            targetList.AddRange(aggregateResult.Notifications);

            return targetList;
        }
    }
}
