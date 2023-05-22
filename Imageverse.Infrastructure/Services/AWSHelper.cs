using Amazon.S3;
using Amazon.S3.Model;
using Imageverse.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace Imageverse.Infrastructure.Services
{
    public class AWSHelper : IAWSHelper
    {
        private readonly IConfiguration _configuration;
        private readonly IAmazonS3 _s3Client;
        private readonly string _s3BucketName;
        private readonly double? _presignedUrlExpiryDays;

        public AWSHelper(IConfiguration configuration, IAmazonS3 s3Client)
        {
            _configuration = configuration;
            _s3Client = s3Client;
            _s3BucketName = _configuration.GetValue<string>("S3BucketName") ?? "imageverse";
            _presignedUrlExpiryDays = _configuration.GetValue<double?>("S3PresignedUrlExpiryDays") ?? 2;
        }

        public async Task UploadFileAsync(IFormFile file, string key)
        {
            PutObjectRequest request = new()
            {
                BucketName = _s3BucketName,
                Key = key,
                InputStream = file.OpenReadStream()
            };
            request.Metadata.Add("Content-Type", file.ContentType);
            await _s3Client.PutObjectAsync(request);
        }

        public string GetPresignedUrlForResource(string key)
        {
            GetPreSignedUrlRequest urlRequest = new()
            {
                BucketName = _s3BucketName,
                Key = key,
                Expires = DateTime.UtcNow.AddDays(_presignedUrlExpiryDays!.Value)
            };
            return _s3Client.GetPreSignedURL(urlRequest);
        }

        public string RegeneratePresignedUrlForResourceIfUrlExpired(string url, string key, out bool expired)
        {
            expired = false;
            if (!url.Contains("amazonaws")) return string.Empty;
            string urlCreatedAt = url.Split(new string[] { "X-Amz-Date" }, StringSplitOptions.None)[1].Split('&')[0].Substring(1);
            string fileExtenstion = url.Split('?')[0].Split('/')[4].Split('.')[1];

            DateTime expiry = DateTime.ParseExact(urlCreatedAt, "yyyyMMddTHHmmssZ", CultureInfo.InvariantCulture, DateTimeStyles.None).AddDays(_presignedUrlExpiryDays!.Value);
            if(expiry < DateTime.UtcNow)
            {
                expired = true;
                return GetPresignedUrlForResource($"{key}.{fileExtenstion}");
            }
            return url;
        }

        public async Task<byte[]> GetFileBytesFromS3Async(string key)
        {
            byte[] fileBytes;
            GetObjectResponse s3Object = await _s3Client.GetObjectAsync(_s3BucketName, key);

            using (var ms = new MemoryStream())
            {
                s3Object.ResponseStream.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return fileBytes;
        }

        public async Task DeleteFileAsync(string key)
        {
            await _s3Client.DeleteObjectAsync(_s3BucketName, key);
        }
    }
}
