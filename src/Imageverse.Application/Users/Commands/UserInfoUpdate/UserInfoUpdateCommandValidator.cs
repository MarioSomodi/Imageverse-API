using FluentValidation;

namespace Imageverse.Application.Users.Commands.UserInfoUpdate
{
    public class UserInfoUpdateCommandValidator : AbstractValidator<UserInfoUpdateCommand>
    {
        public UserInfoUpdateCommandValidator()
        {
            RuleFor(uIUC => uIUC.Id)
                .NotEmpty();
        }
    }
}
