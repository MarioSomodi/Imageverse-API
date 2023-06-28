using Imageverse.Domain.UserLimitAggregate;
using Imageverse.Domain.UserLimitAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imageverse.Infrastructure.Persistance.Configurations
{
    public class UserLimitConfigurations : IEntityTypeConfiguration<UserLimit>
    {
        public void Configure(EntityTypeBuilder<UserLimit> builder)
        {
            builder.ToTable("UserLimits");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedNever()
                .HasConversion(
                //Mapping the id into the DB from the Aggregate
                id => id.Value,
                //Mapping the id out of the DB to the Aggregate
                value => UserLimitId.Create(value));
        }
    }
}
