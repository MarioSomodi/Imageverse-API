using FluentValidation;

namespace Imageverse.Application.Packages.Commands.CreatePackage
{
    public class CreatePackageCommandValidator : AbstractValidator<CreatePackageCommand>
    {
        public CreatePackageCommandValidator()
        {
            RuleFor(rC => rC.Name)
             .NotEmpty();
            RuleFor(rC => rC.Price)
             .NotEmpty();
            RuleFor(rC => rC.DailyImageUploadLimit)
             .NotEmpty();
            RuleFor(rC => rC.DailyUploadLimit)
             .NotEmpty();
            RuleFor(rC => rC.UploadSizeLimit)
             .NotEmpty();
        }
    }
}
