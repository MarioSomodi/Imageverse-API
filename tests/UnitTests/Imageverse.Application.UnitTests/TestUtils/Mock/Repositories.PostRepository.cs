using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.PostAggregate;
using Imageverse.Domain.PostAggregate.Entites;
using Imageverse.Domain.PostAggregate.ValueObjects;
using Imageverse.Domain.UserAggregate.ValueObjects;
using Moq;

namespace Imageverse.Application.UnitTests.TestUtils.Mock
{
	public static partial class Repositories
	{
		public static class PostRepository
		{
			public static Mock<IPostRepository> getMockPostRepository()
			{
				Mock<IPostRepository> mockPostRepository = new();
				mockPostRepository.Setup(x => x.FindByIdAsync(It.IsAny<PostId>()))
				.ReturnsAsync((PostId postId) =>
				{
					var post = Post.Create(
						"description",
						UserId.Create(Guid.Parse(Constants.Constants.User.UserId)),
						new List<Image>() { Image.Create("name", "url", 1, "resolution", "jpeg") });
					return post.UpdateId(post, postId);
				}
				);
				return mockPostRepository;
			}
		}
	}
}
