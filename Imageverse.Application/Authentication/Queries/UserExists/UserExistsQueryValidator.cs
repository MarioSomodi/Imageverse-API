using FluentValidation;

namespace Imageverse.Application.Authentication.Queries.UserExists
{
    public class UserExistsQueryValidator : AbstractValidator<UserExistsQuery>
    {
        public UserExistsQueryValidator()
        {
            RuleFor(lQ => lQ.AuthenticationProviderId).NotEmpty();
        }
    }
}
