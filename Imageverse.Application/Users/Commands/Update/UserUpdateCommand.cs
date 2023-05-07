using ErrorOr;
using Imageverse.Domain.UserAggregate;
using MediatR;

namespace Imageverse.Application.Users.Commands.Update
{
    public record UserUpdateCommand(
        string Id,
        string Username,
        string Name,
        string Surname) : IRequest<ErrorOr<User>>;
}