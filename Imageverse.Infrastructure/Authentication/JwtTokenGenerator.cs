using Imageverse.Application.Common.Interfaces.Authentication;
using Imageverse.Application.Common.Interfaces.Services;
using Imageverse.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Imageverse.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtSettings = jwtOptions.Value;
        }

        public string GenerateToken(User user)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256);
            
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.Name),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.Surname)
            };

            var securityToken = new JwtSecurityToken(issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                signingCredentials: signingCredentials,
                expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes));

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
