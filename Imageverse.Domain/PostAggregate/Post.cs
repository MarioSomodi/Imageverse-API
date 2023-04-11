using Imageverse.Domain.Models;
using Imageverse.Domain.PostAggregate.Entites;
using Imageverse.Domain.PostAggregate.ValueObjects;
using Imageverse.Domain.UserAggregate.ValueObjects;

namespace Imageverse.Domain.PostAggregate
{
    public sealed class Post : AggregateRoot<PostId>
    {
        private readonly List<Image> _images = new();
        private readonly List<Hashtag> _hashtags = new();
        
        public string Description { get; }
        public DateTime PostedAtDateTime { get; }
        public DateTime UpdatedAtDateTime { get; }

        public UserId UserId { get; }

        public IReadOnlyList<Image> Images => _images.AsReadOnly();
        public IReadOnlyList<Hashtag> Hashtags => _hashtags.AsReadOnly();

        private Post(
            PostId postId,
            UserId userId,
            string description,
            DateTime postedAtDateTime, DateTime updatedAtDateTime)
        : base (postId) 
        {
            UserId = userId;
            Description = description;
            PostedAtDateTime = postedAtDateTime;
            UpdatedAtDateTime = updatedAtDateTime;
        }

        public static Post Create(
            string description, UserId userId)
        {
            return new(
                PostId.CreateUnique(),
                userId,
                description,
                DateTime.UtcNow,
                DateTime.UtcNow);
        }
    }
}
