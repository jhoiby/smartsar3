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

            foreach (var requirement in requirements)
            {
                bool success = requirement.Test.Invoke();

                if (!success)
                {
                    if (requirement.Exception != null)
                    {
                        throw requirement.Exception;
                    }

                    notifications.AddNotification(
                        requirement.ParamName, requirement.FailureMessage);
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
