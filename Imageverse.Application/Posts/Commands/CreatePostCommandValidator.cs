using FluentValidation;

namespace Imageverse.Application.Posts.Commands
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(cPC => cPC.SaveImageAs).NotEmpty();
            RuleFor(cPC => cPC.Base64Image).NotEmpty();
            RuleFor(cPC => cPC.Hashtags).NotEmpty();
            RuleFor(cPC => cPC.Description).NotEmpty();
        }
    }
}
