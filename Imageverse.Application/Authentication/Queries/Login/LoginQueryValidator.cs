using FluentValidation;

namespace Imageverse.Application.Authentication.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator() {
            RuleFor(lQ => lQ.Email).NotEmpty();
            RuleFor(lQ => lQ.Password).NotEmpty();
        }
    }
}
