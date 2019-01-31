using System;
using System.Collections.Generic;
using System.Text;
using SSar.Contexts.Common.Domain.Entities;
using SSar.Contexts.Common.Domain.Notifications;

namespace SSar.Contexts.Common.Application.Commands
{ 
    public static class AggregateResultExtensions
    {
        public static CommandResult ToCommandResult<TAggregate>(this AggregateResult<TAggregate> aggregateResult)
        where TAggregate : IAggregateRoot
        {
            var commandResult = CommandResult.Success;

            if (! aggregateResult.Succeeded)
            {
                commandResult = CommandResult.Fail(aggregateResult.Notifications, aggregateResult.Exception);
            }

            return commandResult;
        }
    }
}
