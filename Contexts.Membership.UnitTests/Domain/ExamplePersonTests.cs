using System;
using System.Linq;
using SSar.Contexts.Membership.Domain.Entities;
using Xunit;
using Shouldly;
using SSar.Contexts.Common.Notifications;

namespace SSar.Contexts.Membership.UnitTests.Domain
{
    public class ExamplePersonTests
    {
        private readonly string _name = "Elmer Fudd";
        private readonly string _email = "elmerfudd@wackyhunters.com";

        [Fact]
        public void CreateFromData_GivenValidName_SetsNameProperty()
        {
            var examplePerson = ExamplePerson.Create(_name, _email).Aggregate;

            examplePerson.Name.ShouldBe(_name);
        }

        [Fact]
        public void CreateFromData_GivenValidEmail_SetsEmail()
        {
            var examplePerson = ExamplePerson.Create(_name, _email).Aggregate;

            examplePerson.EmailAddress.ShouldBe(_email);
        }

        [Fact]
        public void SetName_returns_AggregateResult()
        {
            var examplePerson = ExamplePerson.Create(_name, _email).Aggregate;

            var result2 = examplePerson.SetName("Orville");

            result2.ShouldBeOfType<AggregateResult<ExamplePerson>>();
        }

        [Fact]
        public void SetEmail_returns_AggregateResult()
        {
            var examplePerson = ExamplePerson.Create(_name, _email).Aggregate;

            var result = examplePerson.SetEmailAddress("orville@first.com");

            result.ShouldBeOfType<AggregateResult<ExamplePerson>>();
        }

        [Fact]
        public void SetEmailAddress_given_null_should_throw_ArgumentNullException_with_param()
        {
            var examplePerson = ExamplePerson.Create(_name, _email).Aggregate;

            var ex = Should.Throw<ArgumentNullException>(() => examplePerson.SetEmailAddress(null));
            ex.ParamName.ShouldBe("emailAddress");
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

            var result = examplePerson.SetName("");

            result.ShouldSatisfyAllConditions(
                () => result.Notifications["Name"].Count.ShouldBe(1),
                () => result.Notifications["Name"].First().Message.ShouldBe("Name is required."));
        }


        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        public void SetName_given_empty_should_not_set_name(string emptyName)
        {
            var examplePerson = ExamplePerson.Create(_name, _email).Aggregate;

            examplePerson.SetName("");

            examplePerson.Name.ShouldBe(_name);
        }

        [Theory]
        [InlineData(" Fred")]
        [InlineData("   Nelson   ")]
        public void SetName_trims_padded_name(string paddedName)
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void James_Hoiby_is_verbotten()
        {
            var aggregate = ExamplePerson.Create("Bob", "james@hoiby.com").Aggregate;

            var result = aggregate.SetName("James Hoiby");

            result.Notifications["Name"].First().Message.ShouldBe("(Test) James Hoiby is not wanted here!");
        }
    }
}
