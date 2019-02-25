using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using SSar.Contexts.Common.Domain.AggregateRoots;
using SSar.Contexts.Common.Domain.Notifications;
using SSar.Contexts.Common.Domain.ValueTypes;

namespace SSar.Contexts.Membership.Domain.AggregateRoots.MemberOrganizations
{
    public class MemberOrganization : AggregateRoot
    {
        private OrganizationName _name;

        private MemberOrganization()
        {
        }

        public OrganizationName Name => _name;

        public static AggregateResult<MemberOrganization> Create(OrganizationName name)
        {
            var memberOrg = new MemberOrganization();
            var notifications = new NotificationList();
            
            memberOrg.SetOrgName(name);

            if (!notifications.HasNotifications)
            {
                memberOrg.AddEvent(new MemberOrganizationCreated(memberOrg.Id, memberOrg.Name));
            }

            return memberOrg.OrNotifications(notifications)
                .AsResult<MemberOrganization>();
        }

        private AggregateResult<MemberOrganization> SetOrgName(OrganizationName name)
        {
            var requirements = RequirementList.Create()
                .AddExceptionRequirement(
                    () => name != null,
                    new ArgumentNullException(nameof(name)));
            
            Action action = () => _name = name;

            return AggregateExecution
                .CheckRequirements(requirements)
                .ExecuteAction(action)
                .ReturnAggregateResult(this);
        }
    }
}
