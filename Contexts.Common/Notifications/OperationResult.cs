using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SSar.Contexts.Common.Notifications
{
    public class OperationResult
    {
        private OperationResultStatus _status;
        private NotificationList _notifications;
        private Exception _exception;
        private dynamic _data;

        private OperationResult()
        {
        }

        //private OperationResult(
        //    OperationResultStatus status, NotificationList notifications, Exception exception = null)
        //{
        //    _status = status;
        //    _notifications = notifications ?? new NotificationList();
        //    _exception = exception;
        //}

        //private OperationResult(dynamic data, OperationResultStatus status)
        //{
        //    _data = data; // Null allowed
        //    _status = status;
        //    _notifications = new NotificationList();
        //}

        public OperationResultStatus Status => _status;
        public NotificationList Notifications => _notifications; // TODO: Return readonly
        public Exception Exception => _exception;
        public dynamic Data => _data;

        public static OperationResult Successful(dynamic data = null)
        {
            var result = new OperationResult
            {
                _status = OperationResultStatus.Successful,
                _data = data, // Null allowed
                _notifications = new NotificationList()
            };

            return result;
        }

        public static OperationResult FromMessage(string message, string fieldKey = "")
        {
            var notificationList = new NotificationList
            {
                new Notification(message, fieldKey)
            };

            return new OperationResult
            {
                _status = OperationResultStatus.HasNotifications,
                _notifications = notificationList
            };
        }

        public static OperationResult FromNotificationList(NotificationList notificationList)
        {
            return new OperationResult
            {
                _status = OperationResultStatus.HasNotifications,
                _notifications = notificationList
            };
        }

        public static OperationResult FromException(Exception exception, string userMessage = "")
        {
            var notificationList = new NotificationList();

            if (userMessage.Length > 0)
            {
                notificationList.Add(new Notification(userMessage, "ExceptionInfo"));
            }

            return new OperationResult
            {
                _status = OperationResultStatus.HasException,
                _notifications = notificationList,
                _exception = exception
            };
        }
    }
}
