using Imageverse.Domain.Models;
using Imageverse.Domain.Post.Entites;
using Imageverse.Domain.Post.ValueObjects;

namespace Imageverse.Domain.Post
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
            string description,
            DateTime postedAtDateTime, DateTime updatedAtDateTime)
        : base (postId) 
        {
            Description = description;
            PostedAtDateTime = postedAtDateTime;
            UpdatedAtDateTime = updatedAtDateTime;
        }

        public static Post Create(
            string description)
        {
            return new(
                PostId.CreateUnique(),
                description,
                DateTime.UtcNow,
                DateTime.UtcNow);
        }
    }
}
