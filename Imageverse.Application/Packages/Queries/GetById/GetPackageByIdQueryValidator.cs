using FluentValidation;

namespace Imageverse.Application.Packages.Queries.GetById
{
    public class GetPackageByIdQueryValidator : AbstractValidator<GetPackageByIdQuery>
    {
        public GetPackageByIdQueryValidator()
        {
            RuleFor(rC => rC.Id)
                .NotEmpty();
        }
    }
}
