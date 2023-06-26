namespace Imageverse.Domain.Common.Utils
{
    public static class ByteConversions
    {
        public static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
    }
}
