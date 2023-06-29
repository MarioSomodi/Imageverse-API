using ErrorOr;
using Imageverse.Application.Posts.Common;
using MediatR;

namespace Imageverse.Application.Posts.Queries.GetPostsPerPage
{
	public record GetPostsPerPageQuery(int Page) : IRequest<ErrorOr<IEnumerable<PostResult>>>;
}
