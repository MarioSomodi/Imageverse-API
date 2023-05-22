using FluentValidation;

namespace Imageverse.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(gUBIQ => gUBIQ.Id)
                .NotEmpty();
        }
    }
}
