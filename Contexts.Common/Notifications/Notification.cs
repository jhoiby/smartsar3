using System;
using System.Collections.Generic;
using System.Text;

namespace SSar.Contexts.Common.Notifications
{
    public struct Notification
    {
        public Notification(string message, string fieldKey = "")
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            ForField = fieldKey ?? "";
        }

        public string Message { get; }
        public string ForField { get; }
    }
}
