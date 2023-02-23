namespace Imageverse.Contracts.Authentication
{
    public record RegisterRequest
    {
        public int PackageId { get; set; }
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ProfileImage { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
