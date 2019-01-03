using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using SSar.Contexts.Common.Notifications;
using Xunit;

namespace SSar.Contexts.Common.UnitTests.Notifications
{
    public class NotificationForFieldTests
    {
        [Fact]
        public void Constructor_sets_properties()
        {
            var notification = new Notification("Some message.", "DesiredField");

            notification.ShouldSatisfyAllConditions(
                () => notification.Message.ShouldBe("Some message."),
                () => notification.ForField.ShouldBe("DesiredField"));
        }

        [Fact]
        public void Throws_argument_null_exception_for_message()
        {
            var ex = Should.Throw<ArgumentNullException>(() => new Notification(null, "FieldName"));
            ex.ParamName.ShouldBe("notification");
        }

        [Fact]
        public void ForField_set_to_empty_string_if_not_provided()
        {
            var notification = new Notification("Some message.");
            
            notification.ForField.ShouldBeEmpty();
        }

        [Fact]
        public void ForField_provided_null_param_sets_empty_string()
        {
            var notification = new Notification("Some message.", null);

            notification.ForField.ShouldBeEmpty(); // Null is not same as empty
        }
    }
}
