using FluentValidation;

namespace Imageverse.Application.Users.Commands.UserEmailUpdate
{
    public class UserEmailUpdateCommandValidator : AbstractValidator<UserEmailUpdateCommand>
    {
        public UserEmailUpdateCommandValidator()
        {
            RuleFor(uEUC => uEUC.Id)
               .NotEmpty();
            RuleFor(uEUC => uEUC.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
