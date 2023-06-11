namespace Imageverse.Domain.Common.Utils
{
    public static class ByteConversions
    {
        public static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        public static async Task<bool> IsBase64String(string base64)
        {
            return await Task.FromResult(CheckIfIsBase64String());
            bool CheckIfIsBase64String()
            {
                Span<byte> buffer = stackalloc byte[base64.Length];
                return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
            }
        }
    }
}
