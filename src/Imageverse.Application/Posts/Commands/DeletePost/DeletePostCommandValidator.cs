using FluentValidation;
using Imageverse.Application.Common.CustomValidators;
using Imageverse.Application.Common.Interfaces.Persistance;

namespace Imageverse.Application.Posts.Commands.DeletePost
{
	public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
	{
		private readonly IPostRepository _postRepository;

		public DeletePostCommandValidator(IPostRepository postRepository)
		{
			_postRepository = postRepository;
			RuleFor(cPC => cPC.Id).Must(GuidValidator.ValidateGuid).WithMessage("Post Id contains invalid Id format, user id should be GUID.");
			When(cPC => GuidValidator.ValidateGuid(cPC.Id), () =>
			{
				RuleFor(mem => mem.Id).MustAsync(async (entity, value, c) => await PostExistsValidator.Exists(_postRepository, value))
				.WithMessage("Post with received post id does not exist.");
			});
		}
	}
}
