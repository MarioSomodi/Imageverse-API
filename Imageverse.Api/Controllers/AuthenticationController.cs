using ErrorOr;
using Imageverse.Application.Authentication.Commands.Register;
using Imageverse.Application.Authentication.Common;
using Imageverse.Application.Authentication.Queries.Login;
using Imageverse.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Imageverse.Api.Controllers
{
    [AllowAnonymous]
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
    }
}
