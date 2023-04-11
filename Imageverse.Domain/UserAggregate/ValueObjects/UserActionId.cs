using Imageverse.Domain.Models;

namespace Imageverse.Domain.UserAggregate.ValueObjects
{
    public sealed class UserActionId : ValueObject
    {
        public Guid Value { get; }

        public UserActionId(Guid value)
        {
            Value = value;
        }

        public static UserActionId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
