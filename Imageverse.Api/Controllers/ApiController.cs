using ErrorOr;
using Imageverse.Api.Common.Http;
using Microsoft.AspNetCore.Mvc;

namespace Imageverse.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ApiController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        protected IActionResult Problem(List<Error> errors)
        {
            // TODO imporove this part after adding model validation
            HttpContext.Items[HttpContextItemKeys.Errors] = errors;
            var firstError = errors[0];
            var statusCode = firstError.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError,
            };
            return Problem(statusCode: statusCode, title: firstError.Description);
        }
    }
}
