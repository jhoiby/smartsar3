using System;
using System.Collections.Generic;
using System.Text;

namespace SSar.Contexts.Common.Notifications
{
    public class NotificationList
    {
        private List<NotificationForField> _notifications;

        public NotificationList()
        {
            _notifications = new List<NotificationForField>();
        }

        // TODO: Make this read only
        public IList<NotificationForField> Notifications => _notifications;

        public void AddNotification(string notificationMessages, string forField)
        {
            _notifications.Add(new NotificationForField(notificationMessages, forField));
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
