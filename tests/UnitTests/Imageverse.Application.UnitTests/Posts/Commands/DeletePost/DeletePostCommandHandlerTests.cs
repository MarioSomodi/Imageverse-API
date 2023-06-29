using FluentAssertions;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Application.Posts.Commands.DeletePost;
using Imageverse.Application.UnitTests.Posts.Commands.TestUtils;
using Imageverse.Application.UnitTests.TestUtils.Mock;
using Imageverse.Domain.PostAggregate;
using Imageverse.Domain.PostAggregate.ValueObjects;
using Moq;

namespace Imageverse.Application.UnitTests.Posts.Commands.DeletePost
{
	public class DeletePostCommandHandlerTests
	{
		private readonly DeletePostCommandHandler _handler;
		private readonly Mock<IUnitOfWork> _mockIUnitOfWork;
		private readonly Mock<IAWSHelper> _mockAwsHelper;
		private readonly Mock<IPostRepository> _mockPostRepository;
		private readonly Mock<IUserRepository> _mockUserRepository;

		public DeletePostCommandHandlerTests()
		{
			_mockIUnitOfWork = MockIUnitOfWork.getMockIUnitOfWork();
			_mockAwsHelper = new();
			_mockPostRepository = Repositories.PostRepository.getMockPostRepository();
			_mockUserRepository = Repositories.UserRepository.getMockUserRepository();
			_handler = new DeletePostCommandHandler(
				_mockIUnitOfWork.Object,
				_mockAwsHelper.Object,
				_mockPostRepository.Object,
				_mockUserRepository.Object);
		}

		[Fact]
		public async Task HandlerDeletionOFPost_ValidPostId_DeleteAndGetSuccess()
		{
			DeletePostCommand deletePostCommand = DeletePostCommandUtils.CreateCommand();

			Post post = (await _mockPostRepository.Object.FindByIdAsync(PostId.Create(Guid.Parse(deletePostCommand.Id))))!;

			var result = await _handler.Handle(deletePostCommand, default);

			result.IsError.Should().BeFalse();

			result.Value.Should().BeTrue();

			_mockPostRepository.Verify(m => m.Delete(post), Times.Once);
			_mockIUnitOfWork.Verify(m => m.CommitAsync(), Times.Once);
		}
	}
}
