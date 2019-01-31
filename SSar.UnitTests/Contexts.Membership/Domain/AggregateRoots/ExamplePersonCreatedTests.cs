using System;
using Shouldly;
using SSar.Contexts.Membership.Domain.AggregateRoots.ExamplePersons;
using Xunit;

namespace SSar.UnitTests.Contexts.Membership.Domain.AggregateRoots
{
    public class ExamplePersonCreatedTests
    {
        [Fact]
        public void Constructor_sets_properties()
        {
            var person = ExamplePerson.Create("bob", "bob@email.com").NewAggregate;

            var @event = new ExamplePersonCreated(person.Id, person.Name, person.EmailAddress);

            @event.ShouldSatisfyAllConditions(
                () => @event.EventId.ShouldNotBe(Guid.Empty),
                () => @event.Name.ShouldBe(person.Name),
                () => @event.EmailAddress.ShouldBe(person.EmailAddress));
        }
    }
}
