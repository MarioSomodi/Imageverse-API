using ErrorOr;
using Imageverse.Application.Posts.Common;
using MediatR;

namespace Imageverse.Application.Posts.Queries.GetById
{
	public record GetPostByIdQuery(string Id) : IRequest<ErrorOr<PostResult>>;
}
