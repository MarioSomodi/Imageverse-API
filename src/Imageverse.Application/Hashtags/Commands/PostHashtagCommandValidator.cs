using FluentValidation;

namespace Imageverse.Application.Hashtags.Commands
{
    public class PostHashtagCommandValidator : AbstractValidator<PostHashtagCommand>
    {
        public PostHashtagCommandValidator()
        {
            RuleFor(pHC => pHC.Names).NotEmpty();
        }
    }
}
