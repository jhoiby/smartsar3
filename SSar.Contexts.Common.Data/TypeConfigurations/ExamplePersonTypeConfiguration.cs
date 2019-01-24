using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SSar.Contexts.Membership.Domain.Entities.ExamplePersons;

namespace SSar.Contexts.Common.Data.TypeConfigurations
{
    public class ExamplePersonTypeConfiguration : IEntityTypeConfiguration<ExamplePerson>
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
