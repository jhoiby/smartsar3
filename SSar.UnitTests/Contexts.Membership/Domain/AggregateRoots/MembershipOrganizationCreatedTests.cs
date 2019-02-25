﻿using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using SSar.Contexts.Common.Domain.ValueTypes;
using SSar.Contexts.Membership.Domain.AggregateRoots.MembershipOrganizations;
using Xunit;

namespace SSar.UnitTests.Contexts.Membership.Domain.AggregateRoots
{
    public class MembershipOrganizationCreatedTests
    {
        [Fact]
        public void Constructor_sets_name_and_id()
        {
            var id = Guid.NewGuid();
            var orgName = new OrganizationName("Tasty Mold, Inc.");

            var @event = new MembershipOrganizationCreated(id, orgName);

            @event.ShouldSatisfyAllConditions(
                () => @event.Id.ShouldBe(id),
                () => @event.Name.ShouldBe(orgName.Name));
        }
    }
}
