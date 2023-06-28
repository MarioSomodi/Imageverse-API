using ErrorOr;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.Common.AppErrors;
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

		public DeletePostCommandHandler(IUnitOfWork unitOfWork, IAWSHelper aWSHelper)
		{
			_unitOfWork = unitOfWork;
			_aWSHelper = aWSHelper;
		}

		public async Task<ErrorOr<bool>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
		{
			if (!Guid.TryParse(request.Id, out var id))
			{
				return Errors.Common.BadRequest("Invalid Id format.");
			}
			if (await _unitOfWork.GetRepository<IPostRepository>().FindByIdAsync(PostId.Create(id)) is not Post post)
			{
				return Errors.Common.NotFound(nameof(Post));
			}

			User user = (await _unitOfWork.GetRepository<IUserRepository>().FindByIdAsync(post.UserId))!;

			await _aWSHelper.DeleteFileAsync($"posts/{user?.Id.Value}/{post.Images.First().Name}.{post.Images.First().Format}");
			_unitOfWork.GetRepository<IPostRepository>().Delete(post);
			var success = await _unitOfWork.CommitAsync();
			return success;
		}
	}
}
