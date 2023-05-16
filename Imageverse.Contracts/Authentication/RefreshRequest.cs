namespace Imageverse.Contracts.Authentication
{
    public record RefreshRequest(string ExpiredAccessToken, string RefreshToken);
}
