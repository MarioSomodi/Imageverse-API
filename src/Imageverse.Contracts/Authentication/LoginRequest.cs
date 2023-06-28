namespace Imageverse.Contracts.Authentication
{
    public record LoginRequest(
        string Email,
        string Password,
        string? AuthenticationProviderId,
        int AuthenticationType);
}
