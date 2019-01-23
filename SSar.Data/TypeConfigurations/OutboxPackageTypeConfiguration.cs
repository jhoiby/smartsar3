using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SSar.Data.Outbox;
using SSar.Domain.Membership.ExamplePersons;

namespace SSar.Data.TypeConfigurations
{
    public class OutboxPackageTypeConfiguration : IEntityTypeConfiguration<OutboxPackage>
    {
        public void Configure(EntityTypeBuilder<OutboxPackage> builder)
        {
            builder.HasKey("PackageId");
        }
    }
}
