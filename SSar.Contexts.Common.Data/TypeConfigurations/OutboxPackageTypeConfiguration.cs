using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SSar.Contexts.Common.Data.Outbox;
using SSar.Contexts.Common.Data.ServiceInterfaces;

namespace SSar.Contexts.Common.Data.TypeConfigurations
{
    public class OutboxPackageTypeConfiguration : IEntityTypeConfiguration<OutboxPackage>
    {
        public void Configure(EntityTypeBuilder<OutboxPackage> builder)
        {
            builder.HasKey("PackageId");
        }
    }
}
