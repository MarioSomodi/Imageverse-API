using FluentValidation;

namespace Imageverse.Application.Posts.Commands.DeletePost
{
	public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
	{
        public DeletePostCommandValidator()
        {
            RuleFor(dPC => dPC.Id).NotEmpty();
        }
    }
}
