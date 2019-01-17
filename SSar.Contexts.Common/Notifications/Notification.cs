using System;
using System.Collections.Generic;
using System.Text;

namespace SSar.Contexts.Common.Notifications
{
    public class Notification
    {
        private readonly string _message;

        public Notification(string message)
        {
            _message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public string Message => _message;
    }
}
