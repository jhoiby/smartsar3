using System;
using System.Collections.Generic;
using System.Text;
using Shouldly; 
using SSar.Contexts.Common.Domain.ValueTypes;
using Xunit;

namespace SSar.UnitTests.Contexts.Common.ValueTypes
{
    public class PersonNameTests
    {
        private readonly string _firstName = "Wiley";
        private readonly string _lastName = "Coyote";
        private readonly string _nickname = "Bill";

        [Fact]
        public void Constructor_sets_properties()
        {
            var personName = new PersonName(_firstName, _lastName, _nickname);

            personName.ShouldSatisfyAllConditions(
                () => personName.FirstName.ShouldBe(_firstName),
                () => personName.LastName.ShouldBe(_lastName),
                () => personName.Nickname.ShouldBe(_nickname),
                () => personName.FullName.ShouldBe(_firstName + " " + _lastName));
        }

        [Fact]
        public void Constructor_trims_properties()
        {
            var personName = new PersonName(
                " " + _firstName, _lastName + "  ", _nickname + "\n");

            personName.ShouldSatisfyAllConditions(
                () => personName.FirstName.ShouldBe(_firstName),
                () => personName.LastName.ShouldBe(_lastName),
                () => personName.Nickname.ShouldBe(_nickname));
        }

        [Fact]
        public void Constructor_throws_if_FirstName_null()
        {
            Should.Throw<ArgumentNullException>(
                    () => new PersonName(null, _lastName, _nickname))
                .ParamName.ShouldBe("firstName");
        }

        [Fact]
        public void Constructor_throws_if_LastName_null()
        {
            Should.Throw<ArgumentNullException>(
                    () => new PersonName(_firstName, null, _nickname))
                .ParamName.ShouldBe("lastName");
        }

        [Fact]
        public void Constructor_throws_if_Nickname_null()
        {
            Should.Throw<ArgumentNullException>(
                    () => new PersonName(_firstName, _lastName, null))
                .ParamName.ShouldBe("nickname");
        }

        [Fact]
        public void FullName_returns_concatenated_names()
        {
            var personName = new PersonName(_firstName, _lastName, _nickname);

            personName.FullName.ShouldBe(_firstName + " " + _lastName);
        }
    }
}
