using FluentValidation;

namespace Imageverse.Application.Posts.Commands.EditPost
{
	public class EditPostCommandValidator : AbstractValidator<EditPostCommand>
	{
        public EditPostCommandValidator()
        {
			RuleFor(ePC => ePC.Id).NotEmpty();
			RuleFor(ePC => ePC.Description).NotEmpty();
            RuleFor(ePC => ePC.Hashtags).NotEmpty();
		}
	}
}
