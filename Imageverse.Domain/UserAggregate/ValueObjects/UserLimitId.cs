using Imageverse.Domain.Models;

namespace Imageverse.Domain.UserAggregate.ValueObjects
{
    public sealed class UserLimitId : ValueObject
    {
        public Guid Value { get; }

        public UserLimitId(Guid value)
        {
            Value = value;
        }

        public static UserLimitId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
