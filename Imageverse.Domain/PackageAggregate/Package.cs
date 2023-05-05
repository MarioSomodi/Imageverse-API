using Imageverse.Domain.Models;
using Imageverse.Domain.PackageAggregate.ValueObjects;

namespace Imageverse.Domain.PackageAggregate
{
    public sealed class Package : AggregateRoot<PackageId, Guid>
    {
        public string Name { get; private set; }
        public double Price { get; private set; }
        /// <summary>
        /// Specifies the highest upload size for an image in MB
        /// </summary>
        public int UploadSizeLimit { get; private set; }
        /// <summary>
        /// Specifies total amount of MB that user can upload daily
        /// </summary>
        public int DailyUploadLimit { get; private set; }
        /// <summary>
        /// Specifies the amount of images a user can upload daily
        /// </summary>
        public int DailyImageUploadLimit { get; private set; }

        private Package(
            PackageId packageId,
            string name,
            double price,
            int uploadSizeLimit,
            int dailyUploadLimit,
            int dailyImageUploadLimit)
            : base(packageId)
        {
            Name = name;
            Price = price;
            UploadSizeLimit = uploadSizeLimit;
            DailyUploadLimit = dailyUploadLimit;
            DailyImageUploadLimit = dailyImageUploadLimit;
        }

        public static Package Create(
            string name,
            double price,
            int uploadSizeLimit,
            int dailyUploadLimit,
            int dailyImageUploadLimit)
        {
            return new(
                PackageId.CreateUnique(),
                name,
                price,
                uploadSizeLimit,
                dailyUploadLimit,
                dailyImageUploadLimit);
        }

#pragma warning disable CS8618
        private Package()
        {
        }
#pragma warning restore CS8618 
    }
}
