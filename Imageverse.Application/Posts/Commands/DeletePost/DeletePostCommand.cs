using ErrorOr;
using MediatR;

namespace Imageverse.Application.Posts.Commands.DeletePost
{
	public record DeletePostCommand(string Id) : IRequest<ErrorOr<bool>>;
}
