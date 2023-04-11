using Imageverse.Domain.Models;

namespace Imageverse.Domain.UserAggregate.ValueObjects
{
    public sealed class UserStatisticsId : ValueObject
    {
        public Guid Value { get; }

        public UserStatisticsId(Guid value)
        {
            Value = value;
        }

        public static UserStatisticsId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
