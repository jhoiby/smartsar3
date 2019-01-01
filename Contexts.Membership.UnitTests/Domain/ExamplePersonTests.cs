using System;
using SSar.Contexts.Membership.Domain.Entities;
using Xunit;
using Shouldly;

namespace SSar.Contexts.Membership.UnitTests.Domain
{
    public class ExamplePersonTests
    {
        private readonly string _name = "Elmer Fudd";
        private readonly string _email = "elmerfudd@wackyhunters.com";

        [Fact]
        public void CreateFromData_GivenValidName_SetsNameProperty()
        {
            var examplePerson = ExamplePerson.CreateFromData(_name, _email);

            examplePerson.Name.ShouldBe(_name);
        }

        [Fact]
        public void CreateFromData_GivenValidEmail_SetsEmail()
        {
            var examplePerson = ExamplePerson.CreateFromData(_name, _email);

            examplePerson.EmailAddress.ShouldBe(_email);
        }

        [Fact]
        public void SetName_given_null_should_throw_ArgumentNullException_with_param()
        {
            var examplePerson = ExamplePerson.CreateFromData(_name, _email);

            var ex = Should.Throw<ArgumentNullException>(() => examplePerson.SetName(null));
            ex.ParamName.ShouldBe("name");
        }

        [Fact]
        public void SetName_given_empty_name_should_return_error()
        {
            var examplePerson = ExamplePerson.CreateFromData(_name, _email);
            var result = examplePerson.SetName("");

            result.Errors["Name"].ShouldBe("Name is required.");
        }

        [Fact]
        public void SetEmailAddress_given_null_should_throw_ArgumentNullException_with_param()
        {
            var examplePerson = ExamplePerson.CreateFromData(_name, _email);

            var ex = Should.Throw<ArgumentNullException>(() => examplePerson.SetEmailAddress(null));
            ex.ParamName.ShouldBe("emailAddress");
        }

        [Fact]
        public void SetEmailAddres_given_empty_string_should_return_correct_error()
        {
            var examplePerson = ExamplePerson.CreateFromData(_name, _email);

            var result = examplePerson.SetEmailAddress("");

            result.Errors["EmailAddress"].ShouldBe("Email address is required.");
        }

        [Fact]
        public void SetEmailAddress_given_populated_string_returns_successful_result()
        {
            var examplePerson = ExamplePerson.CreateFromData(_name, _email);

            var result = examplePerson.SetEmailAddress("James.Kirk@starfleet.un");

            result.Successful.ShouldBeTrue();
        }

        [Fact]
        public void Errors_should_be_cleared_between_calls()
        {
            var examplePerson = ExamplePerson.CreateFromData(_name, _email);

            var result1 = examplePerson.SetName("");
            result1.Successful.ShouldBe(false);
            
            var result2 = examplePerson.SetName("Bugs");
            result2.Successful.ShouldBe(true);

            var result3 = examplePerson.SetEmailAddress("");
            result3.Successful.ShouldBe(false);

            var result4 = examplePerson.SetEmailAddress("xyz@abc.com");
            result4.Successful.ShouldBe(true);
        }
    }
}
