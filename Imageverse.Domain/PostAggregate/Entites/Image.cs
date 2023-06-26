using Imageverse.Domain.Models;
using Imageverse.Domain.PostAggregate.ValueObjects;

namespace Imageverse.Domain.PostAggregate.Entites
{
    public sealed class Image : Entity<ImageId>
    {
        public string Name { get; private set; }
        public string Url { get; private set; }
        /// <summary>
        /// Specifies image size in MB
        /// </summary>
        public double Size { get; private set; }
        public string Resolution { get; private set; }
        public string Format { get; private set; }

        private Image(
            ImageId imageId,
            string name,
            string url,
            double size,
            string resolution,
            string format) : base(imageId)
        {
            Name = name;
            Url = url;
            Size = size;
            Resolution = resolution;
            Format = format;
        }

        public static Image Create(
            string name,
            string url,
            double size,
            string resolution,
            string format)
        {
            return new(
                ImageId.CreateUnique(),
                name,
                url,
                size,
                resolution,
                format);
        }

        public Image UpdateImageUrl(Image imageToUpdate, string url)
        {
            imageToUpdate.Url = url;
            return imageToUpdate;
        }

#pragma warning disable CS8618
        private Image()
        {
        }
#pragma warning restore CS8618 
    }
}
