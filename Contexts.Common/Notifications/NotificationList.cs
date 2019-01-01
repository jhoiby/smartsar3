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

        public IList<NotificationForField> Notifications => _notifications;

        public void AddNotificationForField(string notificationMessages, string forField)
        {
            _notifications.Add(new NotificationForField(notificationMessages, forField));
        }
    }
}
