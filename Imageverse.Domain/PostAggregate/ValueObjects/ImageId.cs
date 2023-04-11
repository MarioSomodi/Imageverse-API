using Imageverse.Domain.Models;

namespace Imageverse.Domain.PostAggregate.ValueObjects
{
    public sealed class ImageId : ValueObject
    {
        public Guid Value { get; }

        public ImageId(Guid value)
        {
            Value = value;
        }

        public static ImageId CreateUnique()
        {
            return new (Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
