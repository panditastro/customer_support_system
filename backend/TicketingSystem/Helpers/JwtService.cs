using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TicketingSystem.Models;

namespace TicketingSystem.Helpers
{
    /// <summary>
    /// Service for generating JWT tokens
    /// </summary>
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Generate JWT token for authenticated user
        /// </summary>
        public string GenerateToken(User user)
        {
            // 🔐 Claims (VERY IMPORTANT)
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()), // enum → string
                new Claim("id", user.Id.ToString())
            };

            // 🔐 Key validation
            var keyString = _config["Jwt:Key"];
            if (string.IsNullOrEmpty(keyString) || keyString.Length < 32)
                throw new Exception("JWT Key must be at least 32 characters");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(keyString)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 🔐 Token creation
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}