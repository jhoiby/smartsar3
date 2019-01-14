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

        // TODO: Review these test for re-implementation
        //[Fact]
        //public void SetName_given_empty_name_should_return_error()
        //{
        //    var examplePerson = ExamplePerson.CreateFromData(_name, _email);
        //    var result = examplePerson.SetName("");

        //    result.Errors["Name"].ShouldBe("Name is required.");
        //}

        //[Fact]
        //public void SetEmailAddres_given_empty_string_should_return_correct_error()
        //{
        //    var examplePerson = ExamplePerson.CreateFromData(_name, _email);

        //    var result = examplePerson.SetEmailAddress("");

        //    result.Errors["EmailAddress"].ShouldBe("Email address is required.");
        //}

        //[Fact]
        //public void SetEmailAddress_given_populated_string_returns_successful_result()
        //{
        //    var examplePerson = ExamplePerson.CreateFromData(_name, _email);

        //    var result = examplePerson.SetEmailAddress("James.Kirk@starfleet.un");

        //    result.Successful.ShouldBeTrue();
        //}

        //[Fact]
        //public void Notifications_should_be_cleared_between_calls()
        //{
        //    var examplePerson = ExamplePerson.CreateFromData(_name, _email);

        //    var result1 = examplePerson.SetName("");
        //    result1.Successful.ShouldBe(false);

        //    var result2 = examplePerson.SetName("Bugs");
        //    result2.Successful.ShouldBe(true);

        //    var result3 = examplePerson.SetEmailAddress("");
        //    result3.Successful.ShouldBe(false);

        //    var result4 = examplePerson.SetEmailAddress("xyz@abc.com");
        //    result4.Successful.ShouldBe(true);
        //}
    }
}
