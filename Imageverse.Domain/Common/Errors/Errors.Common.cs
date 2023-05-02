using ErrorOr;

namespace Imageverse.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Common
        {
            public static Error NotFound(string resource)
            {
                return Error.NotFound("Common.NotFound", $"Resource : {resource}, that you have requested was not found.");
            }
        }
    }
}
