using ErrorOr;
using Imageverse.Application.Posts.Common;
using MediatR;

namespace Imageverse.Application.Posts.Commands.CreatePost
{
    public record CreatePostCommand(
        string UserId,
        string Description,
        string Base64Image,
        IEnumerable<string> Hashtags,
        string SaveImageAs) : IRequest<ErrorOr<PostResult>>;
}
