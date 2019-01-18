using System.Linq;
using Shouldly;
using SSar.Contexts.Common.Notifications;
using SSar.Contexts.Membership.Domain.AggregateRoots.ExamplePerson;
using Xunit;

namespace SSar.Contexts.Membership.UnitTests.Domain.AggregateRoots
{
    public class ExamplePersonTests
    {
        private readonly string _name = "Elmer Fudd";
        private readonly string _email = "elmerfudd@wackyhunters.com";

        [Fact]
        public void CreateFromNameAndEmail_GivenValidName_SetsNameProperty()
        {
            var examplePerson = ExamplePerson.Create(_name, _email).Aggregate;

            examplePerson.Name.ShouldBe(_name);
        }

        [Fact]
        public void CreateFromNameAndEmail_GivenValidEmail_SetsEmail()
        {
            var examplePerson = ExamplePerson.Create(_name, _email).Aggregate;

            examplePerson.EmailAddress.ShouldBe(_email);
        }

        [Fact]
        public void CreateFromNameAndEmail_GivenValidData_ReturnsAggregate()
        {
            var result = ExamplePerson.Create(_name, _email);

            result.ShouldSatisfyAllConditions(
                () => result.Aggregate.ShouldNotBeNull(),
                () => result.Aggregate.ShouldBeOfType<ExamplePerson>());
        }

        [Fact]
        public void CreateFromNameAndEmail_GivenEmptyData_ReturnsNotifications()
        {
            var result = ExamplePerson.Create(null, _email);

            result.ShouldSatisfyAllConditions(
                () => result.Notifications.Count.ShouldBe(1),
                () => result.Notifications["Name"].First().Message.ShouldBe("Name is required."));
        }



        [Fact]
        public void CreateFromNameAndEmail_adds_ExamplePersonCreated_event()
        {
            var aggregate = ExamplePerson.Create("Elmer Fudd", "elmer@wascallywabbit.com").Aggregate;

            var createdEvent = aggregate.Events.OfType<ExamplePersonCreated>().SingleOrDefault();

            createdEvent.ShouldSatisfyAllConditions(
                () => createdEvent.ShouldNotBeNull(),
                () => createdEvent.Name.ShouldBe("Elmer Fudd"));
        }

        [Fact]
        public void SetName_returns_AggregateResult_with_name()
        {
            var examplePerson = ExamplePerson.Create(_name, _email).Aggregate;

            var result = examplePerson.SetName("Orville");

            result.ShouldSatisfyAllConditions(
                () => result.ShouldBeOfType<AggregateResult<ExamplePerson>>(),
                () => result.Aggregate.Name.ShouldBe("Orville")
                );
        }

        [Fact]
        public void SetEmailAddress_returns_AggregateResult_with_email()
        {
            var examplePerson = ExamplePerson.Create(_name, _email).Aggregate;

            var result = examplePerson.SetEmailAddress("orville@first.com");

            result.ShouldSatisfyAllConditions(
                () => result.ShouldBeOfType<AggregateResult<ExamplePerson>>(),
                () => result.Aggregate.EmailAddress.ShouldBe("orville@first.com")
            );
        }

        [Fact]
        public void SetEmailAddress_given_null_should_return_notification()
        {
            var examplePerson = ExamplePerson.Create(_name, _email).Aggregate;

            var result = examplePerson.SetEmailAddress(null);

            result.ShouldSatisfyAllConditions(
                () => result.Notifications["EmailAddress"].Count.ShouldBe(1),
                () => result.Notifications["EmailAddress"].First().Message.ShouldBe("Email address is required."));
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        public void SetEmailAddress_given_empty_should_return_notification(string emptyEmail)
        {
            var examplePerson = ExamplePerson.Create(_name, _email).Aggregate;

            var result = examplePerson.SetEmailAddress(emptyEmail);

            result.ShouldSatisfyAllConditions(
                () => result.Notifications["EmailAddress"].Count.ShouldBe(1),
                () => result.Notifications["EmailAddress"].First().Message.ShouldBe("Email address is required."));
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        public void SetEmailAddress_given_empty_should_not_change_email(string emptyEmail)
        {
            var examplePerson = ExamplePerson.Create(_name, _email).Aggregate;

            examplePerson.SetName(emptyEmail);

            examplePerson.EmailAddress.ShouldBe(_email);
        }

        [Theory]
        [InlineData(" fred@flintstones.com")]
        [InlineData("   fred@flintstones.com   ")]
        public void SetEmailAddress_trims_padded_email(string paddedEmail)
        {
            var examplePerson = ExamplePerson.Create(_name, _email).Aggregate;

            examplePerson.SetEmailAddress(paddedEmail);

            examplePerson.EmailAddress.ShouldBe(paddedEmail.Trim());
        }

        [Fact]
        public void SetName_given_null_should_return_notification()
        {
            var examplePerson = ExamplePerson.Create(_name, _email).Aggregate;

            var result = examplePerson.SetName(null);

            result.ShouldSatisfyAllConditions(
                () => result.Notifications["Name"].Count.ShouldBe(1),
                () => result.Notifications["Name"].First().Message.ShouldBe("Name is required."));
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        public void SetName_given_empty_should_return_notification(string emptyName)
        {
            var examplePerson = ExamplePerson.Create(_name, _email).Aggregate;

            var result = examplePerson.SetName(emptyName);

            result.ShouldSatisfyAllConditions(
                () => result.Notifications["Name"].Count.ShouldBe(1),
                () => result.Notifications["Name"].First().Message.ShouldBe("Name is required."));
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        public void SetName_given_empty_should_not_change_name(string emptyName)
        {
            var examplePerson = ExamplePerson.Create(_name, _email).Aggregate;

            examplePerson.SetName(emptyName);

            examplePerson.Name.ShouldBe(_name);
        }

        [Theory]
        [InlineData(" Fred")]
        [InlineData("   Nelson   ")]
        public void SetName_trims_padded_name(string paddedName)
        {
            var examplePerson = ExamplePerson.Create(_name, _email).Aggregate;

            examplePerson.SetName(paddedName);

            examplePerson.Name.ShouldBe(paddedName.Trim());
        }

        // A temporary test for fun
        [Fact]
        public void James_Hoiby_is_verbotten()
        {
            var aggregate = ExamplePerson.Create("Bob", "james@hoiby.com").Aggregate;

            var result = aggregate.SetName("James Hoiby");

            result.Notifications["Name"].First().Message.ShouldBe("James Hoiby is not wanted here!");
        }
    }
}
