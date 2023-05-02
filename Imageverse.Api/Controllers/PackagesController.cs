using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Imageverse.Api.Controllers
{
    public class PackagesController : ApiController
    {

        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public PackagesController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return Ok(Array.Empty<string>());
        }
    }
}
