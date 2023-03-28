using Microsoft.AspNetCore.Mvc;

namespace Imageverse.Api.Controllers
{
    public class HashtagsController : ApiController
    {
        [HttpGet]
        public IActionResult ListHashtags()
        {
            return Ok(Array.Empty<string>());
        }
    }
}
