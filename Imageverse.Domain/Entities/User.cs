namespace Imageverse.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ProfileImage { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
