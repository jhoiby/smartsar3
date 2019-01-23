using System;
using Shouldly;
using SSar.Infrastructure.DomainEvents;
using Xunit;

namespace SSar.UnitTests.Infrastructure.DomainEvents
{
    public class DomainEventTests
    {
        // Need concrete class to test abstract
        private class ConcreteDomainEvent : DomainEvent
        {
        }

        [Fact]
        public void Should_be_assignable_to_IDomainEvent()
        {
            var domEvent = new ConcreteDomainEvent();
            domEvent.ShouldBeAssignableTo<IDomainEvent>();
        }

        [Fact]
        public void Should_return_non_default_id()
        {
            var domEvent = new ConcreteDomainEvent();
            domEvent.EventId.ShouldNotBe(Guid.Empty);
        }

        [Fact]
        public void OccurredAtUtc_should_be_about_now()
        {
            var domEvent = new ConcreteDomainEvent();
            domEvent.OccurredAtUtc.ShouldBe(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        }

        [Fact]
        public void EventVersion_should_be_positive_integer()
        {
            var domEvent = new ConcreteDomainEvent();
            domEvent.ShouldSatisfyAllConditions(
                () => domEvent.EventVersion.ShouldBeOfType<int>(),
                () => domEvent.EventVersion.ShouldBeGreaterThan(0));
        }
    }
}
