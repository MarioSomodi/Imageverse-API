using FluentValidation;
using Imageverse.Application.Common.CustomValidators;
using Imageverse.Application.Common.Interfaces.Persistance;

namespace Imageverse.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        private readonly IUserRepository _userRepository;

		public CreatePostCommandValidator(IUserRepository userRepository)
		{
			_userRepository = userRepository;
			RuleFor(cPC => cPC.UserId).Must(GuidValidator.ValidateGuid).WithMessage("User Id contains invalid Id format, user id should be GUID.");
			When(cPC => GuidValidator.ValidateGuid(cPC.UserId), () =>
			{
				RuleFor(mem => mem.UserId).MustAsync(async (entity, value, c) => await UserExistsValidator.Exists(_userRepository, value))
				.WithMessage("User with received user id does not exist.");
				RuleFor(cPC => cPC.SaveImageAs).NotEmpty();
				RuleFor(cPC => cPC.Base64Image).NotEmpty().Matches("^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$");
				RuleFor(cPC => cPC.Hashtags).NotEmpty();
				RuleFor(cPC => cPC.Description).NotEmpty();
			});
		}
	}
}
