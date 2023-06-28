using Microsoft.AspNetCore.Mvc;

namespace Imageverse.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]")]
    public class ErrorController : ApiController
    {
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
