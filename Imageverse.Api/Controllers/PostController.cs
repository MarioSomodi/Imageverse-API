using MapsterMapper;
using MediatR;

namespace Imageverse.Api.Controllers
{
    public class PostController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public PostController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

    }
}
