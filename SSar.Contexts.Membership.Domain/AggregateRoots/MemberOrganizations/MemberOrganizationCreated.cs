using SSar.Contexts.Common.Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Text;
using SSar.Contexts.Common.Domain.ValueTypes;
using SSar.Contexts.Common.Helpers;

namespace SSar.Contexts.Membership.Domain.AggregateRoots.MemberOrganizations
{
    [Serializable]
    public class MemberOrganizationCreated : DomainEvent
    {
        public MemberOrganizationCreated(Guid id, OrganizationName name)
        {
           Id = id.Require(nameof(id));
           FullName = name.FullName.Require(nameof(name.FullName));
           ShortName = name.ShortName.Require(nameof(name.ShortName));
           ReportingCode = name.ReportingCode.Require(nameof(name.ReportingCode));
        }

        public Guid Id { get;  }
        public string FullName { get; }
        public string ShortName { get; }
        public string ReportingCode { get; }
    }
}

