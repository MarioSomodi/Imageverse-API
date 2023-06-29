using ErrorOr;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.PostAggregate;
using Imageverse.Domain.PostAggregate.ValueObjects;
using Imageverse.Domain.UserAggregate;
using MediatR;

namespace Imageverse.Application.Posts.Commands.DeletePost
{
	public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, ErrorOr<bool>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IAWSHelper _aWSHelper;
		private readonly IPostRepository _postRepository;
		private readonly IUserRepository _userRepository;

		public DeletePostCommandHandler(IUnitOfWork unitOfWork, IAWSHelper aWSHelper, IPostRepository postRepository, IUserRepository userRepository)
		{
			_unitOfWork = unitOfWork;
			_aWSHelper = aWSHelper;
			_postRepository = postRepository;
			_userRepository = userRepository;
		}

		public async Task<ErrorOr<bool>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
		{
			Post post = (await _postRepository.FindByIdAsync(PostId.Create(Guid.Parse(request.Id))))!;

			User user = (await _userRepository.FindByIdAsync(post.UserId))!;

			await _aWSHelper.DeleteFileAsync($"posts/{user?.Id.Value}/{post.Images.First().Name}.{post.Images.First().Format}");
			_postRepository.Delete(post);
			var success = await _unitOfWork.CommitAsync();
			return success;
		}
	}
}
