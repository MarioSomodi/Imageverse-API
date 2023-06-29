using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.PostAggregate;
using Imageverse.Domain.PostAggregate.ValueObjects;

namespace Imageverse.Infrastructure.Persistance.Repositories
{
    internal class PostRepository : Repository<Post, PostId>, IPostRepository
    {
        public PostRepository(ImageverseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
