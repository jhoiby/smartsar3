using SSar.Contexts.Common.Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Text;
using SSar.Contexts.Common.Domain.ValueTypes;

namespace SSar.Contexts.Membership.Domain.AggregateRoots.MembershipOrganizations
{
    [Serializable]
    public class MembershipOrganizationCreated : DomainEvent
    {
        public MembershipOrganizationCreated(Guid id, OrganizationName name)
        {
            Id = id;
            Name = name.Name;
        }
        public Guid Id { get;  }
        public string Name { get; }
    }
}

