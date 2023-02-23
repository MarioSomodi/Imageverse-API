﻿namespace Imageverse.Contracts.Authentication
{
    public record AuthenticationResponse
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public string Username { get; set; }= null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ProfileImage { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
