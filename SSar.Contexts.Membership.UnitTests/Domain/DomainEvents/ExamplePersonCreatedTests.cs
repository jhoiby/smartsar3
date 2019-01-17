using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Shouldly;
using SSar.Contexts.Membership.Domain.DomainEvents;
using SSar.Contexts.Membership.Domain.Entities;
using Xunit;

namespace SSar.Contexts.Membership.UnitTests.Domain.DomainEvents
{
    public class ExamplePersonCreatedTests
    {
        [Fact]
        public void Constructor_sets_properties()
        {
            var person = ExamplePerson.Create("bob", "bob@email.com").Aggregate;

            var @event = new ExamplePersonCreated(person, "My description");

            @event.ShouldSatisfyAllConditions(
                () => @event.EventId.ShouldNotBe(Guid.Empty),
                () => @event.ExamplePerson.ShouldBe(person),
                () => @event.Description.ShouldBe("My description"));
        }

        [Fact]
        public void Empty_description_sets_default_description()
        {
            var person = ExamplePerson.Create("bob", "bob@email.com").Aggregate;

            var @event = new ExamplePersonCreated(person);

            @event.Description.Length.ShouldBeGreaterThan(0);
        }
    }
}
