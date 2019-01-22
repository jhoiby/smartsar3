using System;

namespace SSar.Infrastructure.Notifications
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
