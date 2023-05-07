using ErrorOr;
using Imageverse.Application.Users.Commands.Update;
using Imageverse.Contracts.User;
using Imageverse.Domain.UserAggregate;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Imageverse.Api.Controllers
{
    public class UserController : ApiController
    {

        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public UserController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateRequest updateRequest)
        {
            UserUpdateCommand userUpdateCommand = _mapper.Map<UserUpdateCommand>(updateRequest);

            ErrorOr<User> result = await _mediator.Send(userUpdateCommand);

            return result.Match(
                result => Ok(_mapper.Map<UserResponse>(result)),
                errors => Problem(errors));
        }
    }
}
