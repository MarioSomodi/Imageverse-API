using ErrorOr;
using Imageverse.Domain.UserAggregate;
using MediatR;

namespace Imageverse.Application.Users.Commands.UserInfoUpdate
{
    public record UserInfoUpdateCommand(
        string Id,
        string Username,
        string Name,
        string Surname) : IRequest<ErrorOr<User>>;
}