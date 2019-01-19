using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SSar.Contexts.Membership.Domain.AggregateRoots.ExamplePerson;

namespace SSar.Contexts.Membership.Data.TypeConfigurations
{
    class ExamplePersonTypeConfiguration : IEntityTypeConfiguration<ExamplePerson>
    {
        public void Configure(EntityTypeBuilder<ExamplePerson> builder)
        {
            builder.HasKey("_id");

            builder.Property(b => b.Name)
                .HasField("_name");

            builder.Property(b => b.EmailAddress)
                .HasField("_emailAddress");
        }
    }
}
