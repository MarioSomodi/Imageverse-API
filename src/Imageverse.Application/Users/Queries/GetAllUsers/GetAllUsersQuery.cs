using ErrorOr;
using Imageverse.Domain.UserAggregate;
using MediatR;

namespace Imageverse.Application.Users.Queries.GetAllUsers
{
    public record GetAllUsersQuery() : IRequest<ErrorOr<IEnumerable<User>>>;
}
