using Imageverse.Application.Services.Authentication;
using Imageverse.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Imageverse.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public ActionResult Register(RegisterRequest registerRequest)
        {
            AuthenticationResult authenticationResult = _authenticationService.Register(
                registerRequest.PackageId,
                registerRequest.Username,
                registerRequest.Name,
                registerRequest.Surname,
                registerRequest.Email,
                registerRequest.ProfileImage,
                registerRequest.Password);
            AuthenticationResponse authenticationResponse = new AuthenticationResponse
            {
                Id = authenticationResult.Id,
                PackageId = authenticationResult.PackageId,
                Username = authenticationResult.Username,
                Name = authenticationResult.Name,
                Surname = authenticationResult.Surname,
                Email = authenticationResult.Email,
                ProfileImage = authenticationResult.ProfileImage,
                Token = authenticationResult.Token
            };
            return Ok(authenticationResponse);
        }

        [HttpPost]
        public ActionResult Login(LoginRequest loginRequest)
        {
            AuthenticationResult authenticationResult = _authenticationService.Login(loginRequest.Email, loginRequest.Password);
            AuthenticationResponse authenticationResponse = new AuthenticationResponse
            {
                Id = authenticationResult.Id,
                PackageId = authenticationResult.PackageId,
                Username = authenticationResult.Username,
                Name = authenticationResult.Name,
                Surname = authenticationResult.Surname,
                Email = authenticationResult.Email,
                ProfileImage = authenticationResult.ProfileImage,
                Token = authenticationResult.Token
            };
            return Ok(authenticationResponse);
        }
    }
}
