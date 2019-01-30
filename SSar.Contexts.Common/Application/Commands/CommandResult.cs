using System;
using System.Collections.Generic;
using System.Linq;
using SSar.Contexts.Common.Domain.Notifications;

namespace SSar.Contexts.Common.Application.Commands
{
    public class CommandResult
    {
        public bool Succeeded { get; private set; }
        public NotificationList Notifications { get; private set; }
        public Exception Exception { get; private set; }
        public CommandResultStatus Status { get; private set; }
        
        public static CommandResult Success => new CommandResult
        {
            Succeeded = true,
            Status = CommandResultStatus.Succeeded,
            Notifications = new NotificationList()
        };

        public static CommandResult Fail(NotificationList notifications, Exception exception)
        {
            var result = new CommandResult
                {
                    Succeeded = false,
                    Status = exception == null
                        ? CommandResultStatus.ValidationError
                        : CommandResultStatus.Exception,
                    Notifications = notifications ?? new NotificationList(),
                    Exception = exception
                };

            return result;
        }

        public static CommandResult Fail(NotificationList notifications)
        {
            return CommandResult.Fail(notifications, null);
        }

        public static CommandResult Fail(Exception exception)
        {
            return CommandResult.Fail(new NotificationList(), exception);
        }
    }
}
