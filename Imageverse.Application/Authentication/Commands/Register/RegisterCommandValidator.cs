using FluentValidation;
using Imageverse.Domain.Common.Utils;

namespace Imageverse.Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(rC => rC.Username)
                .NotEmpty();
            RuleFor(rC => rC.Name)
                .NotEmpty();
            RuleFor(rC => rC.Surname)
                .NotEmpty();
            RuleFor(rC => rC.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(rC => rC.ProfileImage)
                .NotEmpty();
            RuleFor(rC => rC.PackageId)
                .NotEmpty();
            RuleFor(rC => rC.Password)
                .NotEmpty()
                .MinimumLength(6)
                .Matches(RegexExpressions.lowercase)
                .Matches(RegexExpressions.uppercase)
                .Matches(RegexExpressions.symbol)
                .Matches(RegexExpressions.digit);
        }
    }
}
