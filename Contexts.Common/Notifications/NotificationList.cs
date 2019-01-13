using System;
using System.Collections.Generic;
using System.Text;

namespace SSar.Contexts.Common.Notifications
{
    public class NotificationList
    {
        private readonly Dictionary<string, Notification[]> _notifications 
            = new Dictionary<string, Notification[]>();

        private NotificationList(string paramName, Notification[] notifications)
        {
           _notifications.Add(paramName, notifications);
        }

        public static NotificationList FromNotification(string paramName, Notification message)
        {
            return new NotificationList(paramName, new Notification[] {message});
        }

        public Dictionary<string, Notification[]> Notifications => _notifications;
    }
}
