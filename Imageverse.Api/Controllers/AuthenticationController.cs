using ErrorOr;
using Imageverse.Application.Services.Authentication;
using Imageverse.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Imageverse.Api.Controllers
{
    public class AuthenticationController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public IActionResult Register(RegisterRequest registerRequest)
        {
            ErrorOr<AuthenticationResult> authenticationResult = _authenticationService.Register(
                registerRequest.PackageId,
                registerRequest.Username,
                registerRequest.Name,
                registerRequest.Surname,
                registerRequest.Email,
                registerRequest.ProfileImage,
                registerRequest.Password);
            return authenticationResult.Match(
                authenticationResult => Ok(MapToAuthenticationResult(authenticationResult)),
                errors => Problem(errors));
        }


        [HttpPost]
        public IActionResult Login(LoginRequest loginRequest)
        {
            ErrorOr<AuthenticationResult> authenticationResult = _authenticationService.Login(loginRequest.Email, loginRequest.Password);
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
