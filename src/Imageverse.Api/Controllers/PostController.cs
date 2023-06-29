using ErrorOr;
using Imageverse.Api.Common.Aspects;
using Imageverse.Application.Posts.Commands.CreatePost;
using Imageverse.Application.Posts.Commands.DeletePost;
using Imageverse.Application.Posts.Commands.EditPost;
using Imageverse.Application.Posts.Common;
using Imageverse.Application.Posts.Queries.GetById;
using Imageverse.Application.Posts.Queries.GetPostsPerPage;
using Imageverse.Contracts.Common;
using Imageverse.Contracts.Post;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Imageverse.Api.Controllers
{
	[LogToFile]
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

        [HttpGet]
        public async Task<IActionResult> Get([Required] int page)
        {
            GetPostsPerPageQuery getPostsPerPageQuery = new GetPostsPerPageQuery(page);

            ErrorOr<IEnumerable<PostResult>> result = await _mediator.Send(getPostsPerPageQuery);

            return result.Match(
                result => Ok(result.AsQueryable().ProjectToType<PostResponse>()),
                errors => Problem(errors));
        }

        [HttpGet("{id}")]
		public async Task<IActionResult> Get(string id)
		{
			ErrorOr<PostResult> result = await _mediator.Send(new GetPostByIdQuery(id));

			return result.Match(
				result => Ok(_mapper.Map<PostResponse>(result)),
				errors => Problem(errors));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			ErrorOr<bool> result = await _mediator.Send(new DeletePostCommand(id));

			return result.Match(
				result => Ok(new BoolResponse(result)),
				errors => Problem(errors));
		}

		[HttpPut]
		public async Task<IActionResult> UpdatePost(EditPostRequest editPostRequest)
		{
			EditPostCommand editPostCommand = _mapper.Map<EditPostCommand>(editPostRequest);

			ErrorOr<PostResult> result = await _mediator.Send(editPostCommand);

			return result.Match(
				result => Ok(_mapper.Map<PostResponse>(result)),
				errors => Problem(errors));
		}
	}
}
