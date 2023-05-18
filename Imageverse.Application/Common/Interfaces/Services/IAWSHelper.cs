using Microsoft.AspNetCore.Http;

namespace Imageverse.Application.Common.Interfaces.Services
{
    public interface IAWSHelper
    {
        Task DeleteFileAsync(string key);
        string GetPresignedUrlForResource(string key);
        Task UploadFileAsync(IFormFile file, string key);
        string RegeneratePresignedUrlForResourceIfUrlExpired(string url, string key, out bool expired);
    }
}
