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
        private readonly string _validFullName = "Acme Road Runner Seed, Inc.";
        private readonly string _validShortName = "Acme Seed";
        private readonly string _validReportingCode = "ARRS";

        [Fact]
        public void Constructor_should_set_name()
        {
            var orgName = new OrganizationName(_validFullName, _validShortName, _validReportingCode);

            orgName.FullName.ShouldBe(_validFullName);
        }

        [Fact]
        public void Constructor_given_null_name_should_throw_ArgNullException()
        {
            Should.Throw<ArgumentNullException>(() => new OrganizationName(null, _validShortName, _validReportingCode))
                .ParamName.ShouldBe("fullName");
        }

        [Theory]
        [InlineData("Acme Anvils   ")]
        [InlineData(" Acme Anvils")]
        [InlineData("   Acme Anvils")]
        public void Constructor_given_padded_name_should_trim_it(string paddedFullName)
        {
            var orgName = new OrganizationName(paddedFullName, _validShortName, _validReportingCode);

            orgName.FullName.ShouldBe(paddedFullName.Trim());
        }

        [Fact]
        public void ToString_should_return_full_name()
        {
            var orgName = new OrganizationName(_validFullName, _validShortName, _validReportingCode);

            orgName.ToString().ShouldBe(_validFullName);
        }

        [Fact]
        public void Constructor_should_set_ShortName()
        {
            var orgName = new OrganizationName(_validFullName, _validShortName, _validReportingCode);

            orgName.ShortName.ShouldBe(_validShortName);
        }

        [Fact]
        public void Constructor_given_null_ShortName_should_throw_ArgNullException()
        {
            Should.Throw<ArgumentNullException>(() => new OrganizationName(_validFullName, null, _validReportingCode))
                .ParamName.ShouldBe("shortName");
        }

        [Theory]
        [InlineData("Acme Anvils   ")]
        [InlineData(" Acme Anvils")]
        [InlineData("   Acme Anvils")]
        public void Constructor_given_padded_ShortName_should_trim_it(string paddedShortName)
        {
            var orgName = new OrganizationName(_validFullName, paddedShortName, _validReportingCode);

            orgName.ShortName.ShouldBe(paddedShortName.Trim());
        }

        [Fact]
        public void Constructor_should_set_ReportingCode()
        {
            var orgName = new OrganizationName(_validFullName, _validShortName, _validReportingCode);

            orgName.ReportingCode.ShouldBe(_validReportingCode);
        }

        [Fact]
        public void Constructor_given_null_ReportingCode_should_throw_ArgNullException()
        {
            Should.Throw<ArgumentNullException>(() => new OrganizationName(_validFullName, _validShortName, null))
                .ParamName.ShouldBe("reportingCode");
        }

        [Theory]
        [InlineData("Acme Anvils   ")]
        [InlineData(" Acme Anvils")]
        [InlineData("   Acme Anvils")]
        public void Constructor_given_padded_ReportingCode_should_trim_it(string paddedReportingCode)
        {
            var orgName = new OrganizationName(_validFullName, paddedReportingCode, _validReportingCode);

            orgName.ShortName.ShouldBe(paddedReportingCode.Trim());
        }

    }
}
