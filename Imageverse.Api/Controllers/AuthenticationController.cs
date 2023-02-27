using ErrorOr;
using Imageverse.Application.Authentication.Commands.Register;
using Imageverse.Application.Authentication.Common;
using Imageverse.Application.Authentication.Queries.Login;
using Imageverse.Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Imageverse.Api.Controllers
{
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;

        public AuthenticationController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            RegisterCommand registerCommand = new (registerRequest.PackageId,
                registerRequest.Username,
                registerRequest.Name,
                registerRequest.Surname,
                registerRequest.Email,
                registerRequest.ProfileImage,
                registerRequest.Password);
            
            ErrorOr<AuthenticationResult> authenticationResult = await _mediator.Send(registerCommand);
         
            return authenticationResult.Match(
                authenticationResult => Ok(MapToAuthenticationResult(authenticationResult)),
                errors => Problem(errors));
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            LoginQuery loginQuery = new (loginRequest.Email, loginRequest.Password);

            ErrorOr<AuthenticationResult> authenticationResult = await _mediator.Send(loginQuery);
            
            return authenticationResult.Match(
                authenticationResult => Ok(MapToAuthenticationResult(authenticationResult)),
                errors => Problem(errors));
        }
        
        private static AuthenticationResponse MapToAuthenticationResult(AuthenticationResult authenticationResult)
        {
            return new AuthenticationResponse(
                authenticationResult.User.Id,
                authenticationResult.User.PackageId,
                authenticationResult.User.Username,
                authenticationResult.User.Name,
                authenticationResult.User.Surname,
                authenticationResult.User.Email,
                authenticationResult.User.ProfileImage,
                authenticationResult.Token
            );
        }
    }
}
