using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using SSar.Contexts.Common.AbstractClasses;
using SSar.Contexts.Common.Results;
using Xunit;

namespace SSar.Contexts.Common.UnitTests.AbstractClasses
{
    public class AggregateRootTests
    {
        // AggregateRoot is abstract, requiring concrete implementation to test
        // Though AggregateRoot has no Public behavior, the Protected methods and
        // properties are exposed and should be tested.
        private class ConcreteAggregateRoot : AggregateRoot
        {
            public new ErrorDictionary Notifications => base.Errors;
            public new bool HasErrors => base.HasErrors;

            public new void AddError(string key, string value)
            {
                base.AddError(key, value);
            }

            //public new void ClearErrors()
            //{
            //    base.ClearErrors();
            //}

            public new void ThrowIfNullParam(object param, string paramName)
            {
                base.ThrowIfNullParam(param, paramName);
            }

            public new OperationResult OperationResultFromErrors()
            {
                return base.Result();
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

            concreteAggregateRoot.AddError("key1", "value1");
            
            concreteAggregateRoot.ShouldSatisfyAllConditions(
                () => concreteAggregateRoot.Notifications.ShouldContainKey("key1"),
                () => concreteAggregateRoot.Notifications["key1"].ShouldBe("value1")
                );
        }

        [Fact]
        public void HasNotifications_returns_false_if_no_notifications_exist()
        {
            var concreteAggregateRoot = new ConcreteAggregateRoot();

            concreteAggregateRoot.HasErrors.ShouldBe(false);
        }

        [Fact]
        public void HasNotifications_returns_true_if_notifications_exist()
        {
            var concreteAggregateRoot = new ConcreteAggregateRoot();

            concreteAggregateRoot.AddError("key1", "value1");

            concreteAggregateRoot.HasErrors.ShouldBe(true);
        }

        [Fact]
        public void ThrowIfParamNull_should_throw_if_given_null_object()
        {
            var concreteAggregateRoot = new ConcreteAggregateRoot();

            var ex = Should.Throw<ArgumentNullException>(() => concreteAggregateRoot.ThrowIfNullParam(null, "ParamName"));

            ex.ParamName.ShouldBe("ParamName");
        }

        // Hidden as ClearErrors has been switched to Private method
        //[Fact]
        //public void Clear_errors_should_empty_error_dictionary()
        //{
        //    var concreteAggregateRoot = new ConcreteAggregateRoot();

        //    concreteAggregateRoot.AddError("key1", "error1");
        //    concreteAggregateRoot.ClearErrors();

        //    concreteAggregateRoot.HasErrors.ShouldBeFalse();
        //}

        [Fact]
        public void OperationResultFromErrors_returns_result_with_errors()
        {
            var concreteAggregateRoot = new ConcreteAggregateRoot();

            concreteAggregateRoot.AddError("key1", "error1");

            var result = concreteAggregateRoot.OperationResultFromErrors();

            result.Errors["key1"].ShouldBe("error1");
        }
    }
}
