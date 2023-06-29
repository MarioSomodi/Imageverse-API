using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.HashtagAggregate;
using Imageverse.Domain.HashtagAggregate.ValueObjects;

namespace Imageverse.Infrastructure.Persistance.Repositories
{
    public class HashtagRepository : Repository<Hashtag, HashtagId>, IHashtagRepository
    {
        public HashtagRepository(ImageverseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
