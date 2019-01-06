using System;
using System.Collections.Generic;
using System.Text;

namespace SSar.Contexts.Common.Notifications
{
    public interface INotificationResult
    {
        ResultStatus Status { get; }
        NotificationList Notifications { get; }
    }
}
