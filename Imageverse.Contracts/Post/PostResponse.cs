using Imageverse.Contracts.Hashtag;
using Imageverse.Contracts.Images;

namespace Imageverse.Contracts.Post
{
    public record PostResponse(
        string Id,
        string Description,
        string Author,
        DateTime PostedAtDateTime,
        DateTime UpdatedAtDateTime,
        IEnumerable<ImageResponse> Images,
        IEnumerable<HashtagResponse> Hashtags);
}
