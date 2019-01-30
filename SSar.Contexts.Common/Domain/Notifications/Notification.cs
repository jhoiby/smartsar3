using System;

namespace SSar.Contexts.Common.Domain.Notifications
{
    // TODO: Why is this class here? Why not just use a string?

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
