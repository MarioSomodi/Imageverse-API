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

            public static Error MethodNotAllowed(string? customMessage = null)
            {
                var errorMessage = "Method not allowed.";
                if (customMessage != null)
                    errorMessage = $"{errorMessage} {customMessage}";
                return Error.Custom(405, "Common.MethodNotAllowed", errorMessage);
            }
            public static Error Unauthorized(string? customMessage = null)
            {
                var errorMessage = "Unauthorized.";
                if (customMessage != null)
                    errorMessage = $"{errorMessage} {customMessage}";
                return Error.Custom(401, "Common.Unauthorized", errorMessage);
            }
        }
    }
}
