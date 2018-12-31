using SSar.Contexts.Membership.Domain.Entities;
using Xunit;

namespace SSar.Contexts.Membership.UnitTests.Domain
{
    public class ExamplePersonAggregateTests
    {
        private readonly string _name = "Elmer Fudd";
        private readonly string _email = "elmerfudd@wackyhunters.com";

        [Fact]
        public void CreateFromData_GivenValidName_SetsNameProperty()
        {
            // Arrange
            
            // Act
            var sut = ExamplePerson.CreateFromData(_name, _email).Name;

            // Assert
            Assert.Equal(_name, sut);
        }

        [Fact]
        public void CreateFromData_GivenValidEmail_SetsEmail()
        {
            // Arrange

            // Act
            var sut = ExamplePerson.CreateFromData(_name, _email).EmailAddress;

            // Assert
            Assert.Equal(_email, sut);
        }
    }
}
