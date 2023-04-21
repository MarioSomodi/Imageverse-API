using Imageverse.Domain.HashtagAggregate;
using Imageverse.Domain.PackageAggregate;
using Imageverse.Domain.PostAggregate;
using Imageverse.Domain.UserActionAggregate;
using Imageverse.Domain.UserActionLogAggregate;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserLimitAggregate;
using Microsoft.EntityFrameworkCore;

namespace Imageverse.Infrastructure.Persistance
{
    public class ImageverseDbContext : DbContext
    {
        public ImageverseDbContext(DbContextOptions<ImageverseDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserLimit> UserLimits { get; set; } = null!;
        public DbSet<UserActionLog> UserActionLogs { get; set; } = null!;
        public DbSet<UserAction> UserActions { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<Package> Packages { get; set; } = null!;
        public DbSet<Hashtag> Hashtags { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(ImageverseDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
