using System;
using System.Collections.Generic;
using System.Text;

namespace SSar.Contexts.Common.Notifications
{
    public class OperationResult
    {
        private OperationResultStatus _status;
        private NotificationList _notifications;
        private Exception _exception;

        private OperationResult(
            OperationResultStatus status, NotificationList notifications = null, Exception exception = null)
        {
            _status = status;
            _notifications = notifications ?? new NotificationList();
            _exception = exception;
        }

        public OperationResultStatus Status => _status;
        public NotificationList Notifications => _notifications; // TODO: Return readonly
        public Exception Exception => _exception;

        public static OperationResult CreateSuccessful()
        {
            return new OperationResult(OperationResultStatus.Successful);
        }

        public static OperationResult FromMessage(string message, string fieldKey = "")
        {
            var notificationList = new NotificationList();
            notificationList.Add(new Notification(message, fieldKey));

            return new OperationResult(
                OperationResultStatus.HasNotifications, notificationList);
        }

        public static OperationResult FromNotificationList(NotificationList notificationList)
        {
            return new OperationResult(
                OperationResultStatus.HasNotifications, notificationList);
        }

        public static OperationResult FromException(Exception exception, string userMessage = "")
        {
            var notificationList = new NotificationList();

            if (userMessage.Length > 0)
            {
                notificationList.Add(new Notification(userMessage, "ExceptionInfo"));
            }

            return new OperationResult(
                OperationResultStatus.HasException, notificationList, exception);
        }
    }
}
