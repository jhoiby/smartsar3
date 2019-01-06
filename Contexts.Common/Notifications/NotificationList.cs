using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSar.Contexts.Common.Notifications
{
    public class NotificationList : List<Notification>
    {
        public NotificationList AddNotification(string message, string fieldKey= "")
        {
            this.Add(new Notification(message, fieldKey));

            return this;
        }

        public NotificationList AddFromResult(INotificationResult sourceResult)
        {
            this.AddRange(sourceResult.Notifications);

            return this;
        }

        public bool Empty => !this.Any();
    }
}
