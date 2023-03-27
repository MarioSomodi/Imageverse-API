using FluentValidation;

namespace Imageverse.Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(rC => rC.Name).NotEmpty();
            RuleFor(rC => rC.Email).NotEmpty().EmailAddress();
            RuleFor(rC => rC.PackageId).NotEmpty();
            RuleFor(rC => rC.ProfileImage).NotEmpty();

        }
    }
}
