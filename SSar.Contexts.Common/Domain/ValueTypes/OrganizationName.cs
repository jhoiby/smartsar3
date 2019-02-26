using System;
using System.Collections.Generic;
using System.Text;
using SSar.Contexts.Common.Helpers;

namespace SSar.Contexts.Common.Domain.ValueTypes
{
    public class OrganizationName
    {
        public OrganizationName(string fullName, string shortName, string reportingCode)
        {
            FullName = fullName.Require(nameof(fullName)).Trim();
            ShortName = shortName.Require(nameof(shortName)).Trim();
            ReportingCode = reportingCode.Require(nameof(reportingCode)).Trim();
        }

        public string FullName { get; private set; }

        public string ShortName { get; private set; }

        public string ReportingCode { get; private set; }

        public override string ToString() => FullName;
    }
}
