using Imageverse.Application.Posts.Commands.DeletePost;
using Imageverse.Application.UnitTests.TestUtils.Constants;

namespace Imageverse.Application.UnitTests.Posts.Commands.TestUtils
{
	public class DeletePostCommandUtils
	{
		public static DeletePostCommand CreateCommand() =>
			new DeletePostCommand(Constants.User.UserId);
	}
}
