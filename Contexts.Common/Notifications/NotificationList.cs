using System;
using System.Collections.Generic;
using System.Text;

namespace SSar.Contexts.Common.Notifications
{
    public class NotificationList
    {
        private List<Notification> _notifications;

        public NotificationList()
        {
            _notifications = new List<Notification>();
        }

        // TODO: Make this read only
        public IList<Notification> Notifications => _notifications;

        public void AddNotification(string message, string forField)
        {
            _notifications.Add(new Notification(message, forField));
        }

        public void AddNotifications(NotificationList newNotifications)
        {
            _notifications.AddRange(newNotifications.Notifications);
        }

        public void ToNotificationList(NotificationList targetList)
        {
            targetList.AddNotifications(this);
        }
    }
}
