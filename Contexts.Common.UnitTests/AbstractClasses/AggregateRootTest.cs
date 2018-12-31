using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using SSar.Contexts.Common.AbstractClasses;
using SSar.Contexts.Common.Results;
using Xunit;

namespace SSar.Contexts.Common.UnitTests.AbstractClasses
{
    public class AggregateRootTest
    {
        // AggregateRoot is abstract, requiring concrete implementation to test
        // Though AggregateRoot has no Public behavior, the Protected methods and
        // properties are exposed and should be tested.
        private class ConcreteAggregateRoot : AggregateRoot
        {
            public new ErrorDictionary Notifications => base.Errors;
            public new bool HasNotifications => base.HasErrors;

            public new void AddNotification(string key, string value)
            {
                base.AddError(key, value);
            }
        }

        [Fact]
        public void Notifications_is_initialized()
        {
            var concreteAggregateRoot = new ConcreteAggregateRoot();

            concreteAggregateRoot.Notifications.ShouldNotBeNull();
        }

        [Fact]
        public void AddNotification_should_add_to_notification_dictionary()
        {
            var concreteAggregateRoot = new ConcreteAggregateRoot();

            concreteAggregateRoot.AddNotification("key1", "value1");
            
            concreteAggregateRoot.ShouldSatisfyAllConditions(
                () => concreteAggregateRoot.Notifications.ShouldContainKey("key1"),
                () => concreteAggregateRoot.Notifications["key1"].ShouldBe("value1")
                );
        }

        [Fact]
        public void HasNotifications_returns_false_if_no_notifications_exist()
        {
            var concreteAggregateRoot = new ConcreteAggregateRoot();

            concreteAggregateRoot.HasNotifications.ShouldBe(false);
        }

        [Fact]
        public void HasNotifications_returns_true_if_notifications_exist()
        {
            var concreteAggregateRoot = new ConcreteAggregateRoot();

            concreteAggregateRoot.AddNotification("key1", "value1");

            concreteAggregateRoot.HasNotifications.ShouldBe(true);
        }
    }
}
