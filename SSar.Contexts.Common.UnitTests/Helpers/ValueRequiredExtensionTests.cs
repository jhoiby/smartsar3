using System;
using System.Collections.Generic;
using System.Text;
using SSar.Contexts.Common.Helpers;
using Xunit;

namespace SSar.Contexts.Common.UnitTests.Helpers
{
    public class ValueRequiredExtensionTests
    {
        private readonly string _nullString = null;
        private readonly string _defaultString = default(string);
        private readonly string _zeroLengthString = "";

        // STRING VALIDATION TESTS
        //

        [Fact]
        public void Require_GivenString_ReturnsSame()
        {
            string testString = "Where we're going we don't need roads.";
            
            var sut = testString.Require();
            bool equalReference = ReferenceEquals(testString, sut);
            
            Assert.Same(testString, sut);
        }

        [Fact]
        public void Require_GivenNullString_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _nullString.Require());
        }

        [Fact]
        public void Require_GivenDefaultString_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _defaultString.Require());
        }

        [Fact]
        public void Require_GivenZeroLengthString_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _zeroLengthString.Require());
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("\t")]
        [InlineData("\r")]
        [InlineData("\n")]
        public void Require_GivenWhitespace_ThrowsArgumentException(string testString)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => testString.Require());
        }

        // GUID VALIDATION TESTS
        //
        [Fact]
        public void Require_GivenValidGuid_ReturnsEqualGuid()
        {
            Guid _validGuid = Guid.NewGuid();
            
            Guid sut = _validGuid.Require();
            
            Assert.Equal(_validGuid, sut);
        }

        [Fact]
        public void Require_GivenEmptyGuid_ThrowsArgumentException()
        {
            Guid _emptyGuid = Guid.Empty;
            
            Assert.Throws<ArgumentException>(() => _emptyGuid.Require());
        }
    }
}

