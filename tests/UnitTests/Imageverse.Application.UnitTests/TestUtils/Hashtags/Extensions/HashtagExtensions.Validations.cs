using FluentAssertions;
using Imageverse.Application.Hashtags.Commands.PostHashtag;
using Imageverse.Domain.HashtagAggregate;

namespace Imageverse.Application.UnitTests.TestUtils.Hashtags.Extensions
{
    public static partial class HashtagExtensions
	{
		public static void ValidateCreatedFrom(this IEnumerable<Hashtag> hashtags, PostHashtagCommand command)
		{
			command.Names.ToList().ForEach(n => hashtags.ToList().ConvertAll(h => h.Name).Should().Contain(n));
		}
	}
}
