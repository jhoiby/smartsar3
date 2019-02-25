using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using SSar.Contexts.Common.Domain.ValueTypes;
using Xunit;

namespace SSar.UnitTests.Contexts.Common.ValueTypes
{
    public class OrganizationNameTests
    {
        [Fact]
        public void Constructor_should_set_name()
        {
            var name = "Acme Anvils, Inc.";

            var orgName = new OrganizationName(name);

            orgName.Name.ShouldBe(name);
        }

        [Fact]
        public void Constructor_given_null_name_should_throw_ArgNullException()
        {
            Should.Throw<ArgumentNullException>(() => new OrganizationName(null))
                .ParamName.ShouldBe("name");
        }

        [Theory]
        [InlineData("Acme Anvils   ")]
        [InlineData(" Acme Anvils")]
        [InlineData("   Acme Anvils")]
        public void Constructor_given_padded_name_should_trim_it(string paddedName)
        {
            var orgName = new OrganizationName(paddedName);

            orgName.Name.ShouldBe(paddedName.Trim());
        }

        [Fact]
        public void ToString_should_return_name()
        {
            var name = "Acme Anvils, Inc.";

            var orgName = new OrganizationName(name);

            orgName.ToString().ShouldBe(name);
        }
    }
}
