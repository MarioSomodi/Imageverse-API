namespace Imageverse.Contracts.Authentication
{
    public record LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
