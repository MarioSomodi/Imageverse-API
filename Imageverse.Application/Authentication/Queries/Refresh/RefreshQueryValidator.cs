using FluentValidation;

namespace Imageverse.Application.Authentication.Queries.Refresh
{
    public class RefreshQueryValidator : AbstractValidator<RefreshQuery>
    {
        public RefreshQueryValidator()
        {
            RuleFor(rQ => rQ.ExpiredAccessToken)
                .NotEmpty();
            RuleFor(rQ => rQ.RefreshToken)
                .NotEmpty();
        }
    }
}
