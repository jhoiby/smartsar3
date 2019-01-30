﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using SSar.Contexts.Common.Application.Commands;
using SSar.Contexts.Common.Domain.Notifications;

namespace SSar.Contexts.IdentityAuth.Application.Extensions
{
    public static class IdentityResultExtensions
    {
        public static CommandResult ToCommandResult(this IdentityResult identityResult)
        {
            var notifications = new NotificationList();

            foreach (var error in identityResult.Errors)
            {
                notifications.AddNotification(error.Code, error.Description);
            }

            return CommandResult.Fail(notifications);
        }
    }
}
