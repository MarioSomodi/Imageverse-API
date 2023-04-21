using Imageverse.Domain.Models;

namespace Imageverse.Domain.UserActionLogAggregate.ValueObjects
{
    public sealed class UserActionLogId : ValueObject
    {
        public Guid Value { get; }

        public UserActionLogId(Guid value)
        {
            Value = value;
        }

        public static UserActionLogId Create(Guid value)
        {
            return new(value);
        }

        public static UserActionLogId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
