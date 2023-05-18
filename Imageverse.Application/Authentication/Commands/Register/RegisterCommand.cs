﻿using ErrorOr;
using Imageverse.Application.Authentication.Common;
using MediatR;

namespace Imageverse.Application.Authentication.Commands.Register
{
    public record RegisterCommand(
        string Username,
        string Name,
        string Surname,
        string Email,
        string Password,
        string PackageId) : IRequest<ErrorOr<AuthenticationResult>>;
}
