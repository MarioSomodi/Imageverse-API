namespace Imageverse.Contracts.Images
{
    public record ImageResponse(
        string Id,
        string Name,
        string Url,
        string Size,
        string Resolution,
        string Format);
}
