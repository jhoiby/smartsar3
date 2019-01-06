using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
