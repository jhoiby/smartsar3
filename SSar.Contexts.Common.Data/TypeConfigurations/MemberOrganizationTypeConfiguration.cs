using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SSar.Contexts.Membership.Domain.AggregateRoots.ExamplePersons;
using SSar.Contexts.Membership.Domain.AggregateRoots.MemberOrganizations;

namespace SSar.Contexts.Common.Data.TypeConfigurations
{
    public class MemberOrganizationTypeConfiguration : IEntityTypeConfiguration<MemberOrganization>
    {
        public void Configure(EntityTypeBuilder<MemberOrganization> builder)
        {
            builder.HasKey("_id");

            builder.Property(b => b.Name)
                .HasField("_name");
        }
    }
}