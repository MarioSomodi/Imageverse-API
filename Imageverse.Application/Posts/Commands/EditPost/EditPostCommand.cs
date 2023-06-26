using ErrorOr;
using Imageverse.Application.Posts.Common;
using MediatR;

namespace Imageverse.Application.Posts.Commands.EditPost
{
	public record EditPostCommand(
		string Id,
		string Description,
		List<string> Hashtags) : IRequest<ErrorOr<PostResult>>;
}
