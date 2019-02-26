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
        private readonly string _validNickname = "Acme Seed";
        private readonly string _validReportingCode = "ARRS";

        [Fact]
        public void Constructor_should_set_name()
        {
            var orgName = new OrganizationName(_validFullName, _validNickname, _validReportingCode);

            orgName.FullName.ShouldBe(_validFullName);
        }

        [Fact]
        public void Constructor_given_null_name_should_throw_ArgNullException()
        {
            Should.Throw<ArgumentNullException>(() => new OrganizationName(null, _validNickname, _validReportingCode))
                .ParamName.ShouldBe("fullName");
        }

        [Theory]
        [InlineData("Acme Anvils   ")]
        [InlineData(" Acme Anvils")]
        [InlineData("   Acme Anvils")]
        public void Constructor_given_padded_name_should_trim_it(string paddedFullName)
        {
            var orgName = new OrganizationName(paddedFullName, _validNickname, _validReportingCode);

            orgName.FullName.ShouldBe(paddedFullName.Trim());
        }

        [Fact]
        public void ToString_should_return_full_name()
        {
            var orgName = new OrganizationName(_validFullName, _validNickname, _validReportingCode);

            orgName.ToString().ShouldBe(_validFullName);
        }

        [Fact]
        public void Constructor_should_set_Nickname()
        {
            var orgName = new OrganizationName(_validFullName, _validNickname, _validReportingCode);

            orgName.Nickname.ShouldBe(_validNickname);
        }

        [Fact]
        public void Constructor_given_null_Nickname_should_throw_ArgNullException()
        {
            Should.Throw<ArgumentNullException>(() => new OrganizationName(_validFullName, null, _validReportingCode))
                .ParamName.ShouldBe("nickname");
        }

        [Theory]
        [InlineData("Acme Anvils   ")]
        [InlineData(" Acme Anvils")]
        [InlineData("   Acme Anvils")]
        public void Constructor_given_padded_Nickname_should_trim_it(string paddedNickname)
        {
            var orgName = new OrganizationName(_validFullName, paddedNickname, _validReportingCode);

            orgName.Nickname.ShouldBe(paddedNickname.Trim());
        }

        [Fact]
        public void Constructor_should_set_ReportingCode()
        {
            var orgName = new OrganizationName(_validFullName, _validNickname, _validReportingCode);

            orgName.ReportingCode.ShouldBe(_validReportingCode);
        }

        [Fact]
        public void Constructor_given_null_ReportingCode_should_throw_ArgNullException()
        {
            Should.Throw<ArgumentNullException>(() => new OrganizationName(_validFullName, _validNickname, null))
                .ParamName.ShouldBe("reportingCode");
        }

        [Theory]
        [InlineData("Acme Anvils   ")]
        [InlineData(" Acme Anvils")]
        [InlineData("   Acme Anvils")]
        public void Constructor_given_padded_ReportingCode_should_trim_it(string paddedReportingCode)
        {
            var orgName = new OrganizationName(_validFullName, paddedReportingCode, _validReportingCode);

            orgName.Nickname.ShouldBe(paddedReportingCode.Trim());
        }

    }
}
