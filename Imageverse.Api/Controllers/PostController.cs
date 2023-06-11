using ErrorOr;
using Imageverse.Application.Hashtags.Commands;
using Imageverse.Application.Posts.Commands;
using Imageverse.Application.Posts.Common;
using Imageverse.Contracts.Hashtag;
using Imageverse.Contracts.Post;
using Imageverse.Domain.HashtagAggregate;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<IActionResult> Post(CreatePostRequest createPostRequest)
        {
            CreatePostCommand createPostCommand = _mapper.Map<CreatePostCommand>(createPostRequest);

            ErrorOr<PostResult> result = await _mediator.Send(createPostCommand);

            return result.Match(
                result => Ok(_mapper.Map<PostResponse>(result)),
                errors => Problem(errors));
        }

    }
}
