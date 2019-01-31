using System;
using System.Linq;
using Shouldly;
using SSar.Contexts.Common.Domain.Notifications;
using SSar.Contexts.Membership.Domain.AggregateRoots.ExamplePersons;
using Xunit;

namespace SSar.UnitTests.Contexts.Membership.Domain.AggregateRoots
{
    public class ExamplePersonTests
    {
        private readonly string _name = "Elmer Fudd";
        private readonly string _email = "elmerfudd@wackyhunters.com";

        [Fact]
        public void CreateFromNameAndEmail_GivenValidName_SetsNameProperty()
        {
            var examplePerson = ExamplePerson.Create(_name, _email).NewAggregate;

            examplePerson.Name.ShouldBe(_name);
        }

        [Fact]
        public void CreateFromNameAndEmail_GivenValidEmail_SetsEmail()
        {
            var examplePerson = ExamplePerson.Create(_name, _email).NewAggregate;

            examplePerson.EmailAddress.ShouldBe(_email);
        }

        [Fact]
        public void CreateFromNameAndEmail_GivenInvalidNonEmptyEmail_ReturnsNotifications()
        {

            var result = ExamplePerson.Create(_name, "bugbunnyattest.com");

            result.ShouldSatisfyAllConditions(
                () => result.Notifications["EmailAddress"].ShouldContain(
                    n => n.Message.Contains("A valid email address is required.")));
        }

        [Fact]
        public void CreateFromNameAndEmail_GivenValidData_ReturnsAggregate()
        {
            var result = ExamplePerson.Create(_name, _email);

            result.ShouldSatisfyAllConditions(
                () => result.NewAggregate.ShouldNotBeNull(),
                () => result.NewAggregate.ShouldBeOfType<ExamplePerson>());
        }

        [Fact]
        public void CreateFromNameAndEmail_GivenEmptyData_ReturnsNotifications()
        {
            var result = ExamplePerson.Create("", "");

            result.ShouldSatisfyAllConditions(
                () => result.Notifications["Name"].ShouldContain(
                    n => n.Message.Contains("A name is required.")),
                () => result.Notifications["EmailAddress"].ShouldContain(
                    n => n.Message.Contains("A valid email address is required.")));
        }

        [Fact]
        public void CreateFromNameAndEmail_GivenNullName_ThrowsException()
        {
            var ex = Should.Throw<ArgumentNullException>(
                () => ExamplePerson.Create(null, _email));
            ex.ParamName.ShouldBe("name");
        }

        [Fact]
        public void CreateFromNameAndEmail_GivenNullEmailAddress_ThrowsException()
        {
            var ex = Should.Throw<ArgumentNullException>(
                () => ExamplePerson.Create(_name, null));
            ex.ParamName.ShouldBe("emailAddress");
        }

        [Fact]
        public void CreateFromNameAndEmail_adds_ExamplePersonCreated_event()
        {
            var aggregate = ExamplePerson.Create("Elmer Fudd", "elmer@wascallywabbit.com").NewAggregate;

            var createdEvent = aggregate.Events.OfType<ExamplePersonCreated>().SingleOrDefault();

            createdEvent.ShouldSatisfyAllConditions(
                () => createdEvent.ShouldNotBeNull(),
                () => createdEvent.Name.ShouldBe("Elmer Fudd"));
        }
    }
}
