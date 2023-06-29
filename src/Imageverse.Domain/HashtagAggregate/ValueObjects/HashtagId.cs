using Imageverse.Domain.Models;

namespace Imageverse.Domain.HashtagAggregate.ValueObjects
{
    public sealed class HashtagId : ValueObject
    {
        public Guid Value { get; }

        private HashtagId(Guid value)
        {
            Value = value;
        }

        public static HashtagId Create(Guid guid)
        {
            return new(guid);
        }

        public static HashtagId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
