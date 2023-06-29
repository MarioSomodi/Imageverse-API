using Imageverse.Domain.UserActionAggregate.ValueObjects;
using Imageverse.Domain.UserActionLogAggregate;
using Imageverse.Domain.UserActionLogAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imageverse.Infrastructure.Persistance.Configurations
{
    public class UserActionLogConfigurations : IEntityTypeConfiguration<UserActionLog>
    {
        public void Configure(EntityTypeBuilder<UserActionLog> builder)
        {
            builder.ToTable("UserActionLogs");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedNever()
                .HasConversion(
                //Mapping the id into the DB from the Aggregate
                id => id.Value,
                //Mapping the id out of the DB to the Aggregate
                value => UserActionLogId.Create(value));

            //Each user action log references the action it logged
            builder.Property(m => m.ActionId)
                .ValueGeneratedNever()
                .HasConversion(
                id => id.Value,
                value => UserActionId.Create(value));
        }
    }
}
