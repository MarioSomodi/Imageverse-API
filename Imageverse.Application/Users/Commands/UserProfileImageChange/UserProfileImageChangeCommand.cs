using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Imageverse.Application.Users.Commands.UserProfileImageChange
{
    public record UserProfileImageChangeCommand(IFormFile Image, string Id) : IRequest<ErrorOr<bool>>;
}
