using FluentValidation;

namespace Imageverse.Application.Users.Commands.UserProfileImageChange
{
    public class UserProfileImageChangeValidator : AbstractValidator<UserProfileImageChangeCommand>
    {
        public UserProfileImageChangeValidator()
        {
            RuleFor(uPIC => uPIC.Image)
                .NotNull();
            RuleFor(uPIC => uPIC.Id)
                .NotEmpty();
        }
    }
}
