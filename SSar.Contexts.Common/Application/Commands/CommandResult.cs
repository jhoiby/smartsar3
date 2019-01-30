using System;
using System.Collections.Generic;
using System.Linq;
using SSar.Contexts.Common.Domain.Notifications;

namespace SSar.Contexts.Common.Application.Commands
{
    public class CommandResult
    {
        public enum StatusCode
        {
            Succeeded,
            DomainValidationError,
            ArgumentValidationError,
            Exception
        }

        public bool Succeeded { get; private set; }
        public NotificationList Notifications { get; private set; }
        public Exception Exception { get; private set; }
        public StatusCode Status { get; private set; }
        
        public static CommandResult Success => new CommandResult
        {
            Succeeded = true,
            Status = StatusCode.Succeeded,
            Notifications = new NotificationList()
        };

        public static CommandResult Failed(
            StatusCode reason, NotificationList notifications, Exception exception = null)
        {
            if (reason == StatusCode.Succeeded)
            {
                throw new InvalidOperationException("A failed CommandResult cannot have a status code of 'Succeeded'.");
            }

            return new CommandResult
                {
                    Succeeded = false,
                    Status = reason,
                    Notifications = notifications ?? new NotificationList(),
                    Exception = exception
                };
        }

        public static CommandResult Failed(Exception exception)
        {
            return CommandResult.Failed(
                StatusCode.Exception, new NotificationList(), exception);
        }
    }
}
