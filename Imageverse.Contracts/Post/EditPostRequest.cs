namespace Imageverse.Contracts.Post
{
	public record EditPostRequest(string Id,
		string Description,
		List<string> Hashtags);
}
