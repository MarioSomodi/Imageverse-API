using FluentValidation;
using Imageverse.Domain.Common.Utils;

namespace Imageverse.Application.Users.Commands.UserPasswordUpdate
{
    public class UserPasswordUpdateCommandValidator : AbstractValidator<UserPasswordUpdateCommand>
    {
        public UserPasswordUpdateCommandValidator()
        {
            RuleFor(uPUC => uPUC.Id)
                .NotEmpty();
            RuleFor(uPUC => uPUC.CurrentPassword)
                .NotEmpty();
            RuleFor(uPUC => uPUC.NewPassword)
                .NotEmpty()
                .MinimumLength(6)
                    .WithMessage("The lenght of password must be at least 6 characters. You entered {TotalLength} characters.")
                .Matches(RegexExpressions.lowercase)
                    .WithMessage("Password must contain at least one lowercase letter.")
                .Matches(RegexExpressions.uppercase)
                    .WithMessage("Password must contain at least one uppercase letter.")
                .Matches(RegexExpressions.symbol)
                    .WithMessage("Password must contain at least one special symbol.")
                .Matches(RegexExpressions.digit)
                    .WithMessage("Password must contain at least one number.");
        }
    }
}
