using FluentAssertions;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Application.Posts.Commands.CreatePost;
using Imageverse.Application.UnitTests.Posts.Commands.TestUtils;
using Imageverse.Application.UnitTests.TestUtils.Constants;
using Imageverse.Application.UnitTests.TestUtils.Mock;
using Imageverse.Application.UnitTests.TestUtils.Posts.Extensions;
using MediatR;
using Moq;

namespace Imageverse.Application.UnitTests.Posts.Commands.CreatePost
{
	public class CreatePostCommandHandlerTests
	{
		private readonly CreatePostCommandHandler _handler;
		private readonly Mock<IUnitOfWork> _mockIUnitOfWork;
		private readonly Mock<IUserRepository> _mockUserRepository;
		private readonly Mock<IPackageRepository> _mockPackageRepository;
		private readonly Mock<IUserLimitRepository> _mockUserLimitRepository;
		private readonly Mock<IPostRepository> _mockPostRepository;
		private readonly Mock<IHashtagRepository> _mockHashtagRepository;
		private readonly Mock<IAWSHelper> _mockIAWSHelper;
		private readonly Mock<IPublisher> _mockIPublisher;


		public CreatePostCommandHandlerTests()
		{
			_mockIUnitOfWork = MockIUnitOfWork.getMockIUnitOfWork();
			_mockIAWSHelper = new();
			_mockIPublisher = new();
			_mockUserRepository = Repositories.UserRepository.getMockUserRepository();
			_mockPackageRepository = Repositories.PackageRepository.getMockPackageRepository();
			_mockUserLimitRepository = new();
			_mockPostRepository = new();
			_mockHashtagRepository = new();
			_handler = new CreatePostCommandHandler(
				_mockIUnitOfWork.Object,
				_mockIAWSHelper.Object,
				_mockIPublisher.Object,
				_mockUserRepository.Object,
				_mockPackageRepository.Object,
				_mockUserLimitRepository.Object,
				_mockPostRepository.Object,
				_mockHashtagRepository.Object);
		}

		// T1 : System under test - logical component we're testing
		// T2 : Scenario - what we're testing
		// T3 : Expected outcome - what we expect the logical compoenent to do
		[Theory]
		[MemberData(nameof(ValidCreatePostCommand))]
		public async Task HandleCreatePostCommand_WhenPostIsValid_ShouldCreateAndReturnPost(CreatePostCommand createPostCommand)
		{
			var result = await _handler.Handle(createPostCommand, default);

			result.IsError.Should().BeFalse();

			result.Value.ValidateCreatedFrom(createPostCommand);

			_mockPostRepository.Verify(m => m.AddAsync(result.Value.Post), Times.Once);
			_mockIUnitOfWork.Verify(m => m.CommitAsync(), Times.Once);
		}

		public static IEnumerable<object[]> ValidCreatePostCommand()
		{
			yield return new object[] { CreatePostCommandUtils.CreateCommand(Constants.Post.SaveImageAs.Jpeg) };
			yield return new object[] { CreatePostCommandUtils.CreateCommand(Constants.Post.SaveImageAs.Png) };
			yield return new object[] { CreatePostCommandUtils.CreateCommand(Constants.Post.SaveImageAs.Bmp) };
		}
	}
}
