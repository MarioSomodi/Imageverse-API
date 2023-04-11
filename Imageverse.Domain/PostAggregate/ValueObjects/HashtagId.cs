using Imageverse.Domain.Models;

namespace Imageverse.Domain.PostAggregate.ValueObjects
{
    public sealed class HashtagId : ValueObject
    {
        public Guid Value { get; }

        public HashtagId(Guid value)
        {
            Value = value;
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
