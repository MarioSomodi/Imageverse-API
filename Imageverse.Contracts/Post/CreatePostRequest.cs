namespace Imageverse.Contracts.Post
{
    public record CreatePostRequest(
        string UserId,
        string Description,
        string Base64Image,
        IEnumerable<string> Hashtags,
        string SaveImageAs);
}
