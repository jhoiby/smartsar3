using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SSar.Contexts.Common.Notifications
{
    public class NotificationList : Dictionary<string,List<Notification>>
    {
        public NotificationList()
        {
        }

        public NotificationList(string paramName, string paramMessage) 
            : this(paramName, new Notification(paramMessage))
        {
        }

        public NotificationList(string paramName, Notification paramNotification)
        {
            if (paramNotification == null)
            {
                throw new ArgumentNullException(nameof(paramNotification));
            }

            this.Add(paramName, new List<Notification> { paramNotification });
        }

        public NotificationList(string paramName, List<Notification> paramNotifications)
        {
            if (paramNotifications == null)
            {
                throw new ArgumentNullException(nameof(paramNotifications));
            }

            this.Add(paramName, paramNotifications);
        }
        
        public bool HasNotifications => this.Count > 0;

        public NotificationList AddNotification(string paramName, string paramMessage)
        {
            var notification = new Notification(paramMessage);
            this.Add(paramName, AddToNotificationsFromKey(paramName, notification));

            return this;
        }

        private List<Notification> AddToNotificationsFromKey(
            string paramName, Notification notification)
        {
            this.TryGetValue(paramName, out List<Notification> notifications);

            if (notifications == null)
            {
                notifications = new List<Notification>();
            }

            notifications.Add(notification);

            return notifications;
        }
    }
}
