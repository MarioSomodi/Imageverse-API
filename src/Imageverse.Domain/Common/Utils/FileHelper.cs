using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Imageverse.Domain.Common.Utils
{
    public static class FileHelper
    {
        public static byte[] GetBytes(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            return fileBytes;
        }
        public static bool CheckIfFileIsImage(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");
            var png = new byte[] { 137, 80, 78, 71 };
            var jpeg = new byte[] { 255, 216, 255, 224 };
            var jpeg2 = new byte[] { 255, 216, 255, 225 };

            if (png.SequenceEqual(bytes.Take(png.Length))
                || bmp.SequenceEqual(bytes.Take(bmp.Length))
                || jpeg.SequenceEqual(bytes.Take(jpeg.Length))
                || jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return true;
            
            return false;
        }
    }
}
