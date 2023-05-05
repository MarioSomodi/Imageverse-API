using Imageverse.Domain.Models;

namespace Imageverse.Domain.UserLimitAggregate.ValueObjects
{
    public sealed class UserLimitId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        public UserLimitId(Guid value)
        {
            Value = value;
        }

        public static UserLimitId Create(Guid value)
        {
            return new(value);
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
