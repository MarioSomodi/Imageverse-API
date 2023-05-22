using ErrorOr;
using Imageverse.Application.Authentication.Commands.Register;
using Imageverse.Application.Authentication.Commands.RevokeRefreshToken;
using Imageverse.Application.Authentication.Common;
using Imageverse.Application.Authentication.Queries.Login;
using Imageverse.Application.Authentication.Queries.Refresh;
using Imageverse.Contracts.Authentication;
using Imageverse.Contracts.Common;
using Imageverse.Infrastructure.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Imageverse.Api.Controllers
{
    [AllowAnonymous]
    [Route("[controller]/[action]")]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AuthenticationController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            RegisterCommand registerCommand = _mapper.Map<RegisterCommand>(registerRequest);
            
            ErrorOr<AuthenticationResult> authenticationResult = await _mediator.Send(registerCommand);
         
            return authenticationResult.Match(
                authenticationResult => Ok(_mapper.Map<AuthenticationResponse>(authenticationResult)),
                errors => Problem(errors));
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            LoginQuery loginQuery = _mapper.Map<LoginQuery>(loginRequest);

            ErrorOr<AuthenticationResult> authenticationResult = await _mediator.Send(loginQuery);

            return authenticationResult.Match(
                authenticationResult => Ok(_mapper.Map<AuthenticationResponse>(authenticationResult)),
                errors => Problem(errors));
        }

        [HttpPost]
        public async Task<IActionResult> Refresh(RefreshRequest refreshRequest)
        {
            RefreshQuery refreshQuery = _mapper.Map<RefreshQuery>(refreshRequest);

            ErrorOr<string> refreshedAccessToken = await _mediator.Send(refreshQuery);

            return refreshedAccessToken.Match(
                refreshedAccessTokenResult => Ok(new RefreshResponse(refreshedAccessTokenResult)),
                errors => Problem(errors));
        }

        [Authorize]
        [Admin]
        [HttpPost("{id}")]
        public async Task<IActionResult> RevokeRefreshToken(string id)
        {
            RevokeRefreshTokenCommand revokeRefreshTokenCommand = new RevokeRefreshTokenCommand(id);

            ErrorOr<bool> revokeRefreshToken = await _mediator.Send(revokeRefreshTokenCommand);

            return revokeRefreshToken.Match(
                revokeRefreshTokenResult => Ok(new BoolResponse(revokeRefreshTokenResult)),
                errors => Problem(errors));
        }
    }
}
