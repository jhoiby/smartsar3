using System;
using Shouldly;
using SSar.Contexts.Membership.Domain.AggregateRoots.ExamplePerson;
using Xunit;

namespace SSar.Contexts.Membership.UnitTests.Domain.AggregateRoots
{
    public class ExamplePersonCreatedTests
    {
        [Fact]
        public void Constructor_sets_properties()
        {
            var person = ExamplePerson.Create("bob", "bob@email.com").Aggregate;

            var @event = new ExamplePersonCreated(person.Id, person.Name, person.EmailAddress);

            @event.ShouldSatisfyAllConditions(
                () => @event.EventId.ShouldNotBe(Guid.Empty),
                () => @event.Name.ShouldBe(person.Name),
                () => @event.EmailAddress.ShouldBe(person.EmailAddress));
        }
    }
}
