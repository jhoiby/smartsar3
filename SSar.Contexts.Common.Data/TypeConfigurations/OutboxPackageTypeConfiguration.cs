using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SSar.Contexts.Common.Data.ServiceInterfaces;

namespace SSar.Contexts.Common.Data.TypeConfigurations
{
    public class OutboxPackageTypeConfiguration : IEntityTypeConfiguration<IOutboxPackage>
    {
        public void Configure(EntityTypeBuilder<IOutboxPackage> builder)
        {
            builder.HasKey("PackageId");
        }
    }
}
