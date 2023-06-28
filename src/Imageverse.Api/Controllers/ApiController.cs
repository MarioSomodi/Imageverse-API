using ErrorOr;
using Imageverse.Api.Common.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Imageverse.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        protected IActionResult Problem(List<Error> errors)
        {
            if (errors.Count is 0)
            {
                return Problem();
            }

            HttpContext.Items[HttpContextItemKeys.Errors] = errors;
           
            return Problem(errors[0]);
        }

        private IActionResult Problem(Error error)
        {
            var statusCode = error.NumericType switch
            {
                3 => StatusCodes.Status409Conflict,
                4 => StatusCodes.Status404NotFound,
                400 or 2 => StatusCodes.Status400BadRequest,
                405 => StatusCodes.Status405MethodNotAllowed,
                401 => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError,
            };
            return Problem(statusCode: statusCode, title: error.Description);
        }
    }
}
