using Imageverse.Domain.PackageAggregate.ValueObjects;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.Entities;
using Imageverse.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imageverse.Infrastructure.Persistance.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureUsersTable(builder);
            ConfigureUserStatisticsTable(builder);
            ConfigurePostIdsTable(builder);
            ConfigureUserActionLogIdsTable(builder);
            ConfigureUserLimitIdsTable(builder);
        }

        private void ConfigureUserLimitIdsTable(EntityTypeBuilder<User> builder)
        {
            builder.OwnsMany(m => m.UserLimitIds, uLIB =>
            {
                uLIB.ToTable("UserLimitIds");

                uLIB.WithOwner().HasForeignKey("UserId");

                uLIB.HasKey("Id");

                uLIB.Property(d => d.Value)
                    .HasColumnName("UserLimitId")
                    .ValueGeneratedNever();
            });

            //Enforces ef core to write the data to the underlying private field that is used in the Aggregate
            builder.Metadata.FindNavigation(nameof(User.UserLimitIds))! //Tells the compailer that this field will never be null
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigureUserActionLogIdsTable(EntityTypeBuilder<User> builder)
        {
            builder.OwnsMany(m => m.UserActionLogIds, uALIB =>
            {
                uALIB.ToTable("UserActionLogIds");

                uALIB.WithOwner().HasForeignKey("UserId");

                uALIB.HasKey("Id");

                uALIB.Property(d => d.Value)
                    .HasColumnName("UserActionLogId")
                    .ValueGeneratedNever();
            });

            //Enforces ef core to write the data to the underlying private field that is used in the Aggregate
            builder.Metadata.FindNavigation(nameof(User.UserActionLogIds))! //Tells the compailer that this field will never be null
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigurePostIdsTable(EntityTypeBuilder<User> builder)
        {
            builder.OwnsMany(m => m.PostIds, pIB =>
            {
                pIB.ToTable("UserPostIds");

                pIB.WithOwner().HasForeignKey("UserId");

                pIB.HasKey("Id");

                pIB.Property(d => d.Value)
                    .HasColumnName("PostId")
                    .ValueGeneratedNever();
            });

            //Enforces ef core to write the data to the underlying private field that is used in the Aggregate
            builder.Metadata.FindNavigation(nameof(User.PostIds))! //Tells the compailer that this field will never be null
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigureUserStatisticsTable(EntityTypeBuilder<User> builder)
        {
            //Each user owns its statistics
            builder.OwnsOne(m => m.UserStatistics, uS =>
            {
                uS.ToTable("UserStatistics");

                uS.WithOwner().HasForeignKey("UserId");

                uS.HasKey(nameof(UserStatistics.Id), "UserId");

                uS.Property(m => m.Id)
                    .HasColumnName("UserStatisticsId")
                    .ValueGeneratedNever()
                    .HasConversion(
                    //Mapping the id into the DB from the Aggregate
                    id => id.Value,
                    //Mapping the id out of the DB to the Aggregate
                    value => UserStatisticsId.Create(value));
            });
        }

        //private void ConfigureUserActionLogsTable(EntityTypeBuilder<User> builder)
        //{
        //    //Each user owns many UserActionLogs
        //    builder.OwnsMany(m => m.UserActionLogs, uALB =>
        //    {
        //        uALB.ToTable("UserActonsLogs");

        //        uALB.WithOwner().HasForeignKey("UserId");

        //        uALB.HasKey("Id", "UserId");

        //        uALB.Property(uAL => uAL.Id)
        //            .HasColumnName("UserActionLogId")
        //            .ValueGeneratedNever()
        //            .HasConversion(
        //            id => id.Value,
        //            value => UserActionLogId.Create(value));

        //        uALB.Property(m => m.Message)
        //            .HasMaxLength(2000);
        //        //Each UserActionLog owns one UserAction
        //        uALB.OwnsOne(m => m.Action, aB =>
        //        {
        //            aB.ToTable("UserActions");

        //            aB.
        //        });
        //    });
        //}

        //private void ConfigureUserLimitsTable(EntityTypeBuilder<User> builder)
        //{
        //    //Each user owns many UserLimits
        //    builder.OwnsMany(m => m.UserLimits, uLB =>
        //    {
        //        uLB.ToTable("UserLimits");

        //        uLB.WithOwner().HasForeignKey("UserId");

        //        uLB.HasKey("Id", "UserId");

        //        uLB.Property(m => m.Id)
        //            .HasColumnName("UserLimitId")
        //            .ValueGeneratedNever()
        //            .HasConversion(
        //            id => id.Value,
        //            value => UserLimitId.Create(value));
        //    });
        //}

        private void ConfigureUsersTable(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedNever()
                .HasConversion(
                //Mapping the id into the DB from the Aggregate
                id => id.Value,
                //Mapping the id out of the DB to the Aggregate
                value => UserId.Create(value));

            //Each user references their Package
            builder.Property(m => m.PackageId)
                .ValueGeneratedNever()
                .HasConversion(
                id => id.Value,
                value => PackageId.Create(value));
        }
    }
}
