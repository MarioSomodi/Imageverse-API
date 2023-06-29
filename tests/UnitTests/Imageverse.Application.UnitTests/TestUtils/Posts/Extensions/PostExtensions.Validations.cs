using FluentAssertions;
using Imageverse.Application.Posts.Commands.CreatePost;
using Imageverse.Application.Posts.Common;
using Imageverse.Domain.PostAggregate;

namespace Imageverse.Application.UnitTests.TestUtils.Posts.Extensions
{
	public static partial class PostExtensions
	{
		public static void ValidateCreatedFrom(this PostResult postResult, CreatePostCommand command) {
			postResult.Author.Should().NotBeNullOrWhiteSpace();
			postResult.AuthorPhoto.Should().NotBeNullOrWhiteSpace();
			ValidatePost(postResult.Post, command);

			static void ValidatePost(Post post, CreatePostCommand command)
			{
				post.Description.Should().Be(command.Description);
				post.Images.Count().Should().Be(1);
				post.UserId.Value.Should().Be(command.UserId);
			}
		}
	}
}
