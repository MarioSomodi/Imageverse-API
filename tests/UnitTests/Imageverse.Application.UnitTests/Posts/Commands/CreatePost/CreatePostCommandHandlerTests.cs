using FluentAssertions;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Application.Posts.Commands.CreatePost;
using Imageverse.Application.UnitTests.Posts.Commands.TestUtils;
using Imageverse.Application.UnitTests.TestUtils.Constants;
using Imageverse.Application.UnitTests.TestUtils.Posts.Extensions;
using Imageverse.Domain.PackageAggregate;
using Imageverse.Domain.PackageAggregate.ValueObjects;
using Imageverse.Domain.UserAggregate;
using Imageverse.Domain.UserAggregate.Entities;
using Imageverse.Domain.UserAggregate.ValueObjects;
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
			_mockIUnitOfWork = new();
			_mockIAWSHelper = new();
			_mockIPublisher = new();
			_mockUserRepository = new();
			_mockPackageRepository = new();
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
			_mockUserRepository.Setup(x => x.FindByIdAsync(It.IsAny<UserId>()))
			 .ReturnsAsync((UserId userId) => { 
					 var usr = User.Create(
						 "username",
						 "name",
						 "surname",
						 "email",
						 "password",
						 PackageId.CreateUnique(),
						 UserStatistics.Create(1, 1, 1, 0, 1),
						 new byte[0],
						 "refreshToken",
						 "profileImage",
						 DateTime.UtcNow,
						 "0",
						 1);
					 return usr.UpdateId(usr, userId);
				 }
			 );

			_mockPackageRepository.Setup(x => x.FindByIdAsync(It.IsAny<PackageId>()))
			 .ReturnsAsync((PackageId packageId) =>
			 {
				 var package = Package.Create("name", 1, 1, 1, 1);
				 return package.UpdateId(package, packageId);
			 }
			 );

			var result = await _handler.Handle(createPostCommand, default);

			result.IsError.Should().BeFalse();

			result.Value.ValidateCreatedFrom(createPostCommand);

			_mockPostRepository.Verify(m => m.AddAsync(result.Value.Post), Times.Once);
		}

		public static IEnumerable<object[]> ValidCreatePostCommand()
		{
			yield return new object[] { CreatePostCommandUtils.CreateCommand(Constants.Post.SaveImageAs.Jpeg) };
			yield return new object[] { CreatePostCommandUtils.CreateCommand(Constants.Post.SaveImageAs.Png) };
			yield return new object[] { CreatePostCommandUtils.CreateCommand(Constants.Post.SaveImageAs.Bmp) };
		}
	}
}
