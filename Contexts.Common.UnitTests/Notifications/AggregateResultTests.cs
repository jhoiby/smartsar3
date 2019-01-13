using System;
using System.Linq;
using Shouldly;
using SSar.Contexts.Common.AbstractClasses;
using SSar.Contexts.Common.Notifications;
using Xunit;

namespace SSar.Contexts.Common.UnitTests.Notifications
{
    public class AggregateResultTests
    {
        private class TestAggregate : AggregateRoot
        {
        }
    }
}
