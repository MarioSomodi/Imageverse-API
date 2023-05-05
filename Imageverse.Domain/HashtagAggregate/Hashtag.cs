using Imageverse.Domain.HashtagAggregate.ValueObjects;
using Imageverse.Domain.Models;

namespace Imageverse.Domain.HashtagAggregate
{
    public sealed class Hashtag : AggregateRoot<HashtagId>
    {
        public string Name { get; private set; }

        private Hashtag(HashtagId hashtagId, string name) : base(hashtagId)
        {
            Name = name;
        }

        public static Hashtag Create(string name)
        {
            return new(HashtagId.CreateUnique(), name);
        }

#pragma warning disable CS8618 
        private Hashtag()
        {
        }
#pragma warning restore CS8618 
    }
}
