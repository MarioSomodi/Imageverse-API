using ErrorOr;

namespace Imageverse.Domain.Common.AppErrors
{
    public static partial class Errors
    {
        public static class Common
        {
            public static Error NotFound(string resource) => Error.NotFound("Common.NotFound", $"Resource : {resource}, that you have requested was not found.");

            public static Error BadRequest(string? customMessage = null)
            {
                var errorMessage = "Bad Request.";
                if (customMessage != null)
                    errorMessage = $"{errorMessage} {customMessage}"; 
                return Error.Custom(400, "Common.BadRequest", errorMessage);
            }
        }
    }
}
