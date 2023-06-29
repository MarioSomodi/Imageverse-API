using Imageverse.Domain.PostAggregate;
using Imageverse.Domain.PostAggregate.Entites;
using Imageverse.Domain.PostAggregate.ValueObjects;
using Imageverse.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imageverse.Infrastructure.Persistance.Configurations
{
    public class PostConfigurations : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            ConfigurePostsTable(builder);
            ConfigureImagesTable(builder);
            ConfigureHashtagIdsTable(builder);
        }

        private void ConfigureHashtagIdsTable(EntityTypeBuilder<Post> builder)
        {
            builder.OwnsMany(m => m.HashtagIds, hIB =>
            {
                hIB.ToTable("PostHastagIds");

                hIB.WithOwner().HasForeignKey("PostId");

                hIB.HasKey("Id");

                hIB.Property(d => d.Value)
                    .HasColumnName("HashtagId")
                    .ValueGeneratedNever();
            });

            //Enforces ef core to write the data to the underlying private field that is used in the Aggregate
            builder.Metadata.FindNavigation(nameof(Post.HashtagIds))! //Tells the compailer that this field will never be null
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigureImagesTable(EntityTypeBuilder<Post> builder)
        {
            builder.OwnsMany(m => m.Images, iB =>
            {
                iB.ToTable("Images");

                iB.WithOwner().HasForeignKey("PostId");

                iB.HasKey(nameof(Image.Id), "PostId");

                iB.Property(i => i.Id)
                    .HasColumnName("ImageId")
                    .ValueGeneratedNever()
                    .HasConversion(
                    //Mapping the id into the DB from the Aggregate
                    id => id.Value,
                    //Mapping the id out of the DB to the Aggregate
                    value => ImageId.Create(value));
            });

            //Enforces ef core to write the data to the underlying private field that is used in the Aggregate
            builder.Metadata.FindNavigation(nameof(Post.Images))!//Tells the compailer that this field will never be null
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigurePostsTable(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedNever()
                .HasConversion(
                //Mapping the id into the DB from the Aggregate
                id => id.Value,
                //Mapping the id out of the DB to the Aggregate
                value => PostId.Create(value));

            //Each Post has a reference to the user that created it
            builder.Property(m => m.UserId)
                .ValueGeneratedNever()
                .HasConversion(
                id => id.Value,
                value => UserId.Create(value));
        }
    }
}
