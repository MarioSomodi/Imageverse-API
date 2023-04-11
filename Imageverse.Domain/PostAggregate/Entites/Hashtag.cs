using Imageverse.Domain.Models;
using Imageverse.Domain.PostAggregate.ValueObjects;

namespace Imageverse.Domain.PostAggregate.Entites
{
    public sealed class Hashtag : Entity<HashtagId>
    {
        public string Name { get; }

        private Hashtag(HashtagId hashtagId, string name) : base(hashtagId)
        {
            Name = name;
        }

        public static Hashtag Create(string name)
        {
            return new(HashtagId.CreateUnique(), name);
        }
    }
}
