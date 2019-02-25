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
           Name = name.Name.Require(nameof(name));
        }
        public Guid Id { get;  }
        public string Name { get; }
    }
}

