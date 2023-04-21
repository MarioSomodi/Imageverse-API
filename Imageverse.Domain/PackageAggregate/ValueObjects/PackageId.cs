using Imageverse.Domain.Models;

namespace Imageverse.Domain.PackageAggregate.ValueObjects
{
    public sealed class PackageId : ValueObject
    {
        public Guid Value { get; }

        public PackageId(Guid value)
        {
            Value = value;
        }

        public static PackageId Create(Guid value)
        {
            return new(value);
        }

        public static PackageId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
