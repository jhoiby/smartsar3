using System;
using System.Collections.Generic;
using System.Text;

namespace SSar.Contexts.Common.Notifications
{
    public enum ResultStatus
    {
        Successful,
        HasNotifications,
        HasException // Implies HasNotifications
    }
}
