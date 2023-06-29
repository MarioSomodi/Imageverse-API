using FluentValidation;

namespace Imageverse.Application.UserLimits.Queries
{
    public class UserLimitOnDateQueryValidator : AbstractValidator<UserLimitOnDateQuery>
    {
        public UserLimitOnDateQueryValidator()
        {
            RuleFor(uLODQ => uLODQ.Date).NotEmpty();
            RuleFor(uLODQ => uLODQ.Id).NotEmpty();
        }
    }
}
