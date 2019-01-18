using System;
using System.Collections.Generic;
using System.Text;
using SSar.Contexts.Common.Events;

namespace SSar.Contexts.Membership.Domain
{
    public abstract class MembershipDomainEvent : DomainEvent
    {
        public override string Publisher => "Membership";
    }
}
