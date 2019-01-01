using System;
using System.Collections.Generic;
using System.Text;

namespace SSar.Contexts.Common.Notifications
{
    public struct NotificationForField
    {
        public NotificationForField(string notification, string forField = "")
        {
            Notification = notification ?? throw new ArgumentNullException(nameof(notification));
            ForField = forField ?? "";
        }

        public string Notification { get; }
        public string ForField { get; }
    }
}
