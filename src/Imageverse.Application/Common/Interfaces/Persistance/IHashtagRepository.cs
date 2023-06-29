using Imageverse.Domain.HashtagAggregate;
using Imageverse.Domain.HashtagAggregate.ValueObjects;

namespace Imageverse.Application.Common.Interfaces.Persistance
{
    public interface IHashtagRepository : IRepository<Hashtag, HashtagId>
    {
    }
}
