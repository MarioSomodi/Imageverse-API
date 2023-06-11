using Imageverse.Domain.HashtagAggregate.ValueObjects;
using Imageverse.Domain.Models;
using Imageverse.Domain.PostAggregate.Entites;
using Imageverse.Domain.PostAggregate.ValueObjects;
using Imageverse.Domain.UserActionLogAggregate.ValueObjects;
using Imageverse.Domain.UserAggregate.ValueObjects;

namespace Imageverse.Domain.PostAggregate
{
    public sealed class Post : AggregateRoot<PostId>
    {
        private readonly List<Image> _images = new();
        private readonly List<HashtagId> _hashtagIds = new();
        
        public string Description { get; private set; }
        public DateTime PostedAtDateTime { get; private set; }
        public DateTime UpdatedAtDateTime { get; private set; }

        public UserId UserId { get; private set; }

        public IReadOnlyList<Image> Images => _images.AsReadOnly();
        public IReadOnlyList<HashtagId> HashtagIds => _hashtagIds.AsReadOnly();

        private Post(
            PostId postId,
            UserId userId,
            string description,
            DateTime postedAtDateTime, 
            DateTime updatedAtDateTime,
            List<Image> images)
        : base (postId) 
        {
            UserId = userId;
            Description = description;
            PostedAtDateTime = postedAtDateTime;
            UpdatedAtDateTime = updatedAtDateTime;
            _images = images;
        }

        public static Post Create(
            string description,
            UserId userId,
            List<Image> images)
        {
            return new(
                PostId.CreateUnique(),
                userId,
                description,
                DateTime.UtcNow,
                DateTime.UtcNow,
                images);
        }

        public Post AddHashtagIds(Post postToUpdate, List<HashtagId> hashtagIds)
        {
            postToUpdate._hashtagIds.AddRange(hashtagIds);
            return postToUpdate;
        }

        public Post AddImageToPost(Post postToUpdate, Image image)
        {
            postToUpdate._images.Add(image);
            return postToUpdate;
        }

#pragma warning disable CS8618
        private Post()
        {
        }
#pragma warning restore CS8618 
    }
}
