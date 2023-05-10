using FluentValidation;

namespace Imageverse.Application.Users.Commands.UserPackageChange
{
    internal class UserPackageChangeCommandValidator : AbstractValidator<UserPackageChangeCommand>
    {
        public UserPackageChangeCommandValidator()
        {
            RuleFor(uPCC => uPCC.Id)
                .NotEmpty();
            RuleFor(uPCC => uPCC.PackageId)
                .NotEmpty();
        }
    }
}
