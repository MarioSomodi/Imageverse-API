using Imageverse.Domain.Models;
using Imageverse.Domain.Post.ValueObjects;

namespace Imageverse.Domain.User.Entites
{
    public sealed class UserLimit : Entity<UserLimitId>
    {
        public DateTime Date { get; }
        /// <summary>
        /// Specifies total MB of images uploaded on specific date
        /// </summary>
        public int AmountUploaded { get; }
        public int AmountOfImagesUploaded { get; }
        public int RequestedChangeOfPackage { get; }

        private UserLimit(
            UserLimitId userLimitId,
            DateTime date,
            int amountUploaded,
            int amountOfImagesUploaded,
            int requestedChangeOfPackage)
            : base(userLimitId)
        {
            Date = date;
            AmountUploaded = amountUploaded;
            AmountOfImagesUploaded = amountOfImagesUploaded;
            RequestedChangeOfPackage = requestedChangeOfPackage;
        }

        public static UserLimit Create(
            int amountUploaded,
            int amountOfImagesUploaded,
            int requestedChangeOfPackage)
        {
            return new(
                UserLimitId.CreateUnique(),
                DateTime.UtcNow,
                amountUploaded,
                amountOfImagesUploaded,
                requestedChangeOfPackage);
        }
    }
}
