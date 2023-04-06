using Imageverse.Domain.Models;
using Imageverse.Domain.Post.ValueObjects;

namespace Imageverse.Domain.Post.Entites
{
    public sealed class Image : Entity<ImageId>
    {
        public string Name { get; }
        public string Url { get; }
        /// <summary>
        /// Specifies image size in MB
        /// </summary>
        public int Size { get; }
        public string Resolution { get; }
        public string Format { get; }

        private Image(
            ImageId imageId,
            string name,
            string url,
            int size,
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
            int size,
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
    }
}
