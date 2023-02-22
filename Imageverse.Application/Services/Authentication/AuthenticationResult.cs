namespace Imageverse.Application.Services.Authentication
{
    public record AuthenticationResult
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
        public string Token { get; set; }
        public int PackageId { get; set; }
    }
}
