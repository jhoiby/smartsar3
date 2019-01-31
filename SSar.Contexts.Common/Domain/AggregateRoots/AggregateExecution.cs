using System;
using System.Collections.Generic;
using SSar.Contexts.Common.Domain.Notifications;

namespace SSar.Contexts.Common.Domain.AggregateRoots
{
    // TODO this into a service and inject
    // into aggregates. E.g. 'AggregateExecutionService'

    public static class AggregateExecution
    {
        public static NotificationList CheckRequirements(List<AggregateRequirement> requirements)
        {
            var notifications = new NotificationList();

            foreach (var requirementSet in requirements)
            {
                bool success = requirementSet.Test.Invoke();

                if (!success)
                {
                    notifications.AddNotification(
                        requirementSet.ParamName, requirementSet.FailureMessage);
                }
            }

            return notifications;
        }

        public static NotificationList ExecuteAction(
            this NotificationList notifications, Action action)
        {
            if (!notifications.HasNotifications)
            {
                action.Invoke();
            }

            return notifications;
        }

        public static AggregateResult<TAggregate> ReturnAggregateResult<TAggregate>(
            this NotificationList notifications, TAggregate aggregate) 
            where TAggregate : IAggregateRoot
        {
            return aggregate.OrNotifications(notifications)
                .AsResult<TAggregate>();
        }
    }
}
