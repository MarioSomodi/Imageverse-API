namespace Imageverse.Contracts.Authentication
{
    public record RegisterRequest
    {
        public int PackageId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
        public string Password { get; set; }
    }
}
