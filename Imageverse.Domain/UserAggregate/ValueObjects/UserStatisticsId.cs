using Imageverse.Domain.Models;

namespace Imageverse.Domain.UserAggregate.ValueObjects
{
    public sealed class UserStatisticsId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        public UserStatisticsId(Guid value)
        {
            Value = value;
        }

        public static UserStatisticsId Create(Guid value)
        {
            return new(value);
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
