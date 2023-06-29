using Imageverse.Application.Hashtags.Commands.PostHashtag;
using Imageverse.Application.UnitTests.TestUtils.Constants;

namespace Imageverse.Application.UnitTests.Hashtags.Commands.TestUtils
{
    public class PostHashtagCommandUtils
    {
        public static PostHashtagCommand CreateCommand() =>
            new PostHashtagCommand(Constants.Hashtag.Hashtags);
    }
}
