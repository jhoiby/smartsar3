using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.Results;
using SSar.Contexts.Common.AbstractClasses;

namespace SSar.Contexts.Common.Notifications
{
    public static class CommandResultExtensions
    {        
        public static CommandResult FromAggregateResult(this CommandResult cmdResult, AggregateResult<IAggregateRoot> aggregateResult)
        {
            Guid aggregateId = default(Guid);

            if (aggregateResult != null)
            {
                aggregateId = aggregateResult.Aggregate.Id;
            }

            return new CommandResult
                (aggregateResult.Status, aggregateResult.Notifications,
                    aggregateResult.Exception, aggregateId);
        }

        // TODO: This is temporary, will only handle one message per parameter
        public static CommandResult ToCommandResult(this List<ValidationFailure> failures)
        {
            var notifications = new NotificationList();

            foreach (var failure in failures)
            {
                notifications.AddNotification(failure.PropertyName, failure.ErrorMessage);
            }

            return new CommandResult(ResultStatus.HasNotifications, notifications, null);
        }
    }
}
