using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SSar.Domain.IdentityAuth.Entities;
using SSar.Domain.Membership.ExamplePersons;

namespace SSar.Data.TypeConfigurations
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
