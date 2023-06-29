using FluentValidation;

namespace Imageverse.Application.Hashtags.Commands.PostHashtag
{
    public class PostHashtagCommandValidator : AbstractValidator<PostHashtagCommand>
    {
        public PostHashtagCommandValidator()
        {
            RuleFor(pHC => pHC.Names).NotEmpty();
        }
    }
}
