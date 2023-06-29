using Imageverse.Domain.HashtagAggregate;
using Imageverse.Domain.Models;
using Imageverse.Domain.PackageAggregate;
using Imageverse.Domain.PostAggregate;
using Imageverse.Domain.UserActionAggregate;
using Imageverse.Domain.UserActionLogAggregate;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserLimitAggregate;
using Imageverse.Infrastructure.Persistance.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Imageverse.Infrastructure.Persistance
{
    public class ImageverseDbContext : DbContext
    {
        private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;

        public ImageverseDbContext(
            DbContextOptions<ImageverseDbContext> options,
            PublishDomainEventsInterceptor publishDomainEventsInterceptor) : base(options)
        {
            _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
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
                .Ignore<List<IDomainEvent>>()
                .ApplyConfigurationsFromAssembly(typeof(ImageverseDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
