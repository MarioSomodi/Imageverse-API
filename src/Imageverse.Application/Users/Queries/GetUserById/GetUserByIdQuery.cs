using ErrorOr;
using Imageverse.Domain.UserAggregate;
using MediatR;

namespace Imageverse.Application.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(string Id) : IRequest<ErrorOr<User>>;
}
