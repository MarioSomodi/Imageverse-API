using Imageverse.Domain.PostAggregate;
using Imageverse.Domain.PostAggregate.ValueObjects;

namespace Imageverse.Application.Common.Interfaces.Persistance
{
    public interface IPostRepository : IRepository<Post, PostId>
    {
    }
}
