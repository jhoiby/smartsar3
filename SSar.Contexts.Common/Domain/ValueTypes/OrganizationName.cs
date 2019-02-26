using System;
using System.Collections.Generic;
using System.Text;
using SSar.Contexts.Common.Helpers;

namespace SSar.Contexts.Common.Domain.ValueTypes
{
    public class OrganizationName
    {
        public OrganizationName(string fullName, string nickname, string reportingCode)
        {
            FullName = fullName.Require(nameof(fullName)).Trim();
            Nickname = nickname.Require(nameof(nickname)).Trim();
            ReportingCode = reportingCode.Require(nameof(reportingCode)).Trim();
        }

        public string FullName { get; private set; }

        public string Nickname { get; private set; }

        public string ReportingCode { get; private set; }

        public override string ToString() => FullName;
    }
}
