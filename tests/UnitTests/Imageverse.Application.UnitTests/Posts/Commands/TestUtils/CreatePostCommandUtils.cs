using Imageverse.Application.Posts.Commands.CreatePost;
using Imageverse.Application.UnitTests.TestUtils.Constants;

namespace Imageverse.Application.UnitTests.Posts.Commands.TestUtils
{
	public class CreatePostCommandUtils
	{
		public static CreatePostCommand CreateCommand(Constants.Post.SaveImageAs saveImageAs) =>
			new CreatePostCommand(
				Constants.User.UserId.ToString()!,
				Constants.Post.Description,
				Constants.Post.Base64Image,
				Constants.Post.Hashtags,
				GetSaveImageAs(saveImageAs));

		public static string GetSaveImageAs(Constants.Post.SaveImageAs saveImageAs)
		{
			if (saveImageAs == Constants.Post.SaveImageAs.Jpeg) return Constants.Post.SaveImageAsJpeg;
			if (saveImageAs == Constants.Post.SaveImageAs.Png) return Constants.Post.SaveImageAsPng;
			return Constants.Post.SaveImageAsBmp;
		}
	}
}
