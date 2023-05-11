using Imageverse.Domain.Models;
using Imageverse.Domain.UserLimitAggregate.ValueObjects;

namespace Imageverse.Domain.UserLimitAggregate
{
    public sealed class UserLimit : AggregateRoot<UserLimitId>
    {
        public DateTime Date { get; private set; }
        /// <summary>
        /// Specifies total MB of images uploaded on specific date
        /// </summary>
        public int AmountOfMBUploaded { get; private set; }
        public int AmountOfImagesUploaded { get; private set; }
        public bool RequestedChangeOfPackage { get; private set; }

        private UserLimit(
            UserLimitId userLimitId,
            DateTime date,
            int amountUploaded,
            int amountOfImagesUploaded,
            bool requestedChangeOfPackage)
            : base(userLimitId)
        {
            Date = date;
            AmountOfMBUploaded = amountUploaded;
            AmountOfImagesUploaded = amountOfImagesUploaded;
            RequestedChangeOfPackage = requestedChangeOfPackage;
        }

        public static UserLimit Create(
            int amountUploaded,
            int amountOfImagesUploaded,
            bool requestedChangeOfPackage)
        {
            return new(
                UserLimitId.CreateUnique(),
                DateTime.UtcNow,
                amountUploaded,
                amountOfImagesUploaded,
                requestedChangeOfPackage);
        }
        public UserLimit UpdateRequestedChangeOfPackage(UserLimit userLimitToUpdate, bool value)
        {
            userLimitToUpdate.RequestedChangeOfPackage = value;
            return userLimitToUpdate;
        }

        public UserLimit UpdateAmountOfImagesUploaded(UserLimit userLimitToUpdate, int value)
        {
            userLimitToUpdate.AmountOfImagesUploaded = value;
            return userLimitToUpdate;
        }

        public UserLimit UpdateAmountOfMBUploaded(UserLimit userLimitToUpdate, int value)
        {
            userLimitToUpdate.AmountOfMBUploaded = value;
            return userLimitToUpdate;
        }


#pragma warning disable CS8618
        private UserLimit()
        {
        }
#pragma warning restore CS8618 
    }
}
