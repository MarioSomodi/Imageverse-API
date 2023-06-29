using Imageverse.Domain.PackageAggregate;
using Imageverse.Domain.PackageAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imageverse.Infrastructure.Persistance.Configurations
{
    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            builder.ToTable("Packages");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
              .ValueGeneratedNever()
              .HasConversion(
              //Mapping the id into the DB from the Aggregate
              id => id.Value,
              //Mapping the id out of the DB to the Aggregate
              value => PackageId.Create(value));
        }
    }
}
