using Imageverse.Application.Common.Interfaces.Persistance;
using Imageverse.Domain.PostAggregate;
using Imageverse.Domain.PostAggregate.ValueObjects;

namespace Imageverse.Application.Common.CustomValidators
{
	public static class PostExistsValidator
	{
		public static async Task<bool> Exists(IPostRepository postRepository, string postId)
		{
			if (await postRepository.FindByIdAsync(PostId.Create(Guid.Parse(postId))) is not Post user)
			{
				return false;
			}
			return true;
		}
	}
}
