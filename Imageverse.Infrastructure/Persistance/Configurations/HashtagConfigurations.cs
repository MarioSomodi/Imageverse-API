using Imageverse.Domain.HashtagAggregate;
using Imageverse.Domain.HashtagAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imageverse.Infrastructure.Persistance.Configurations
{
    public class HashtagConfigurations : IEntityTypeConfiguration<Hashtag>
    {
        public void Configure(EntityTypeBuilder<Hashtag> builder)
        {
            builder.ToTable("Hashtags");

            builder.HasKey(m =>  m.Id);

            builder.Property(m => m.Id)
              .ValueGeneratedNever()
              .HasConversion(
              //Mapping the id into the DB from the Aggregate
              id => id.Value,
              //Mapping the id out of the DB to the Aggregate
              value => HashtagId.Create(value));
        }
    }
}
