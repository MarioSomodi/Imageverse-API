using FluentValidation;

namespace Imageverse.Application.Users.Commands.UserIsAdminChange
{
    public class UserIsAdminChangeCommandValidator : AbstractValidator<UserIsAdminChangeCommand>
    {
        public UserIsAdminChangeCommandValidator()
        {
            RuleFor(uIACC => uIACC.Id)
                .NotEmpty();
            RuleFor(uIACC => uIACC.IsAdmin)
                .NotNull();
        }
    }
}
