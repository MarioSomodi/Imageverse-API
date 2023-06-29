using FluentAssertions;
using Imageverse.Application.Common.Interfaces;
using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Application.Hashtags.Commands.PostHashtag;
using Imageverse.Application.UnitTests.Hashtags.Commands.TestUtils;
using Imageverse.Application.UnitTests.TestUtils.Hashtags.Extensions;
using Imageverse.Application.UnitTests.TestUtils.Mock;
using Moq;

namespace Imageverse.Application.UnitTests.Hashtags.Commands.PostHashtag
{
    public class PostHashtagCommandHandlerTests
    {
        private readonly Mock<IHashtagRepository> _mockHashtagRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly PostHashtagCommandHandler _handler;

        public PostHashtagCommandHandlerTests()
        {
            _mockHashtagRepository = new();
            _mockUnitOfWork = MockIUnitOfWork.getMockIUnitOfWork();
            _handler = new PostHashtagCommandHandler(_mockUnitOfWork.Object, _mockHashtagRepository.Object);
        }
        [Fact]
        public async Task HandlePostHashtagCommand_WhenHashtagIsValid_ShouldCreateAndReturnHashtags()
        {
            PostHashtagCommand postHashtagCommand = PostHashtagCommandUtils.CreateCommand();

            var result = await _handler.Handle(postHashtagCommand, default);

            result.IsError.Should().BeFalse();

            result.Value.ValidateCreatedFrom(postHashtagCommand);

            _mockHashtagRepository.Verify(m => m.AddRangeAsync(result.Value.ToList()), Times.Once);
            _mockUnitOfWork.Verify(m => m.CommitAsync(), Times.Once);
        }
    }
}
