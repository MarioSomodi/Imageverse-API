using ErrorOr;
using Imageverse.Domain.HashtagAggregate;
using MediatR;

namespace Imageverse.Application.Hashtags.Queries
{
    public record GetAllHashtagsQuery() : IRequest<ErrorOr<IEnumerable<Hashtag>>>;
}
