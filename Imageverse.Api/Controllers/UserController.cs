using ErrorOr;
using Imageverse.Application.Users.Commands.UserEmailUpdate;
using Imageverse.Application.Users.Commands.UserInfoUpdate;
using Imageverse.Application.Users.Commands.UserIsAdminChange;
using Imageverse.Application.Users.Commands.UserPackageChange;
using Imageverse.Application.Users.Commands.UserPasswordUpdate;
using Imageverse.Application.Users.Commands.UserProfileImageChange;
using Imageverse.Contracts.Common;
using Imageverse.Contracts.User;
using Imageverse.Domain.UserAggregate;
using Imageverse.Infrastructure.Authentication;
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

        [HttpPut("Info")]
        public async Task<IActionResult> UpdateInfo(UserInfoUpdateRequest updateRequest)
        {
            UserInfoUpdateCommand userInfoUpdateCommand = _mapper.Map<UserInfoUpdateCommand>(updateRequest);

            ErrorOr<User> result = await _mediator.Send(userInfoUpdateCommand);

            return result.Match(
                result => Ok(_mapper.Map<UserResponse>(result)),
                errors => Problem(errors));
        }

        [HttpPut("Email")]
        public async Task<IActionResult> UpdateEmail(UserEmailUpdateRequest updateRequest)
        {
            UserEmailUpdateCommand userEmailUpdateCommand = _mapper.Map<UserEmailUpdateCommand>(updateRequest);

            ErrorOr<bool> result = await _mediator.Send(userEmailUpdateCommand);

            return result.Match(
                result => Ok(new BoolResponse(result)),
                errors => Problem(errors));
        }

        [HttpPut("Password")]
        public async Task<IActionResult> UpdatePassword(UserPasswordUpdateRequest updateRequest)
        {
            UserPasswordUpdateCommand userPasswordUpdateCommand = _mapper.Map<UserPasswordUpdateCommand>(updateRequest);

            ErrorOr<bool> result = await _mediator.Send(userPasswordUpdateCommand);

            return result.Match(
                result => Ok(new BoolResponse(result)),
                errors => Problem(errors));
        }

        [HttpPut("Package")]
        public async Task<IActionResult> ChangePackage(UserPackageChangeRequest changeRequest)
        {
            UserPackageChangeCommand userPackageChangeCommand = _mapper.Map<UserPackageChangeCommand>(changeRequest);

            ErrorOr<bool> result = await _mediator.Send(userPackageChangeCommand);

            return result.Match(
                result => Ok(new BoolResponse(result)),
                errors => Problem(errors));
        }


        [HttpPut("Image/{id}")]
        public async Task<IActionResult> ChangeProfileImage(IFormFile image, string id)
        {
            UserProfileImageChangeCommand userProfileImageChangeCommand = new UserProfileImageChangeCommand(image, id);

            ErrorOr<bool> result = await _mediator.Send(userProfileImageChangeCommand);

            return result.Match(
                result => Ok(new BoolResponse(result)),
                errors => Problem(errors));
        }

        [Admin]
        [HttpPut("Admin")]
        public async Task<IActionResult> MakeAdmin(UserIsAdminChangeRequest changeRequest)
        {
            UserIsAdminChangeCommand makeUserAdminCommand = _mapper.Map<UserIsAdminChangeCommand>(changeRequest);

            ErrorOr<bool> result = await _mediator.Send(makeUserAdminCommand);

            return result.Match(
                result => Ok(new BoolResponse(result)),
                errors => Problem(errors));
        }
    }
}
