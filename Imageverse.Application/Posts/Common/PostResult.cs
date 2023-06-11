using Imageverse.Domain.HashtagAggregate;
using Imageverse.Domain.PostAggregate;

namespace Imageverse.Application.Posts.Common
{
    public record PostResult(
        Post Post,
        string Author,
        IEnumerable<Hashtag> Hashtags);
}
