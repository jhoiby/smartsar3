using System;
using Shouldly;
using SSar.Contexts.Common.Application.IntegrationEvents;
using Xunit;

namespace SSar.UnitTests.Infrastructure.IntegrationEvents
{
    public class IntegrationEventTests
    {
        private readonly string _boundedContextName = "SSar.UnitTests";

        private class ConcreteIntegrationEvent : IntegrationEvent
        {

            internal ConcreteIntegrationEvent(string boundedContextName, string eventName)
                : base(boundedContextName, eventName)
            {
            }
        }

        [Fact]
        public void Should_be_assignable_from_IIntegrationEvent()
        {
            var intEvent = new ConcreteIntegrationEvent(_boundedContextName, nameof(ConcreteIntegrationEvent));
            intEvent.ShouldBeAssignableTo<IIntegrationEvent>();
        }

        [Fact]
        public void EventId_should_not_be_default_Guid()
        {
            var intEvent = new ConcreteIntegrationEvent(_boundedContextName, nameof(ConcreteIntegrationEvent));
            intEvent.EventId.ShouldNotBe(Guid.Empty);
        }

        [Fact]
        public void OccurredAtUtc_should_be_about_now()
        {
            var intEvent = new ConcreteIntegrationEvent(_boundedContextName, nameof(ConcreteIntegrationEvent));
            intEvent.OccurredAtUtc.ShouldBe(DateTime.UtcNow, TimeSpan.FromSeconds(10));
        }

        [Fact]
        public void EventVersion_should_be_positive_integer()
        {
            var intEvent = new ConcreteIntegrationEvent(_boundedContextName, nameof(ConcreteIntegrationEvent));
            intEvent.EventVersion.ShouldSatisfyAllConditions(
                () => intEvent.EventVersion.ShouldBeOfType<int>(),
                () => intEvent.EventVersion.ShouldBeGreaterThan(0));
        }

        [Fact]
        public void Label_should_be_eventName_from_constructor()
        {
            var intEvent = new ConcreteIntegrationEvent(_boundedContextName, nameof(ConcreteIntegrationEvent));
            intEvent.Label.ShouldBe(nameof(ConcreteIntegrationEvent));
        }

        [Fact]
        public void Publisher_should_be_BC_name_from_constructor()
        {
            var intEvent = new ConcreteIntegrationEvent(_boundedContextName, nameof(ConcreteIntegrationEvent));
            intEvent.Publisher.ShouldBe(_boundedContextName);
        }
    }
}
