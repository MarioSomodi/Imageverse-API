using Imageverse.Domain.HashtagAggregate;
using Imageverse.Domain.Models;
using Imageverse.Domain.PostAggregate.Entites;
using Imageverse.Domain.UserAggregate;

namespace Imageverse.Domain.PostAggregate.Events
{
    public record PostCreated(
        User User,
        List<Hashtag> Hashtags,
        Post Post,
        double ImageSize) : IDomainEvent;
}
