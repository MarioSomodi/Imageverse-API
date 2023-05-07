using FluentValidation;

namespace Imageverse.Application.Users.Commands.Update
{
    public class UserUpdateCommandValidator : AbstractValidator<UserUpdateCommand>
    {
        public UserUpdateCommandValidator()
        {
            RuleFor(rC => rC.Id)
                .NotEmpty();
        }
    }
}
