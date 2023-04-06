using Imageverse.Domain.Models;
using Imageverse.Domain.Post.ValueObjects;

namespace Imageverse.Domain.User.Entites
{
    public sealed class Package : Entity<PackageId>
    {
        public string Name { get; }
        public double Price { get; }
        /// <summary>
        /// Specifies the highest upload size for an image in MB
        /// </summary>
        public int UploadSizeLimit { get; }
        /// <summary>
        /// Specifies total amount of MB that user can upload daily
        /// </summary>
        public int DailyUploadLimit { get; }
        /// <summary>
        /// Specifies the amount of images a user can upload daily
        /// </summary>
        public int DailyImageUploadLimit { get; }

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
    }
}
