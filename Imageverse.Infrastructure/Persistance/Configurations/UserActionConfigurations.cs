using Imageverse.Domain.UserActionAggregate;
using Imageverse.Domain.UserActionAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imageverse.Infrastructure.Persistance.Configurations
{
    public class UserActionConfigurations : IEntityTypeConfiguration<UserAction>
    {
        public void Configure(EntityTypeBuilder<UserAction> builder)
        {
            builder.ToTable("UserActions");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
              .ValueGeneratedNever()
              .HasConversion(
              //Mapping the id into the DB from the Aggregate
              id => id.Value,
              //Mapping the id out of the DB to the Aggregate
              value => UserActionId.Create(value));
        }
    }
}
