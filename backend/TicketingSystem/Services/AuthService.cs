
using TicketingSystem.DTOs;
using TicketingSystem.Helpers;
using TicketingSystem.Models;
using TicketingSystem.Repositories.Interfaces;
using TicketingSystem.Services.Interfaces;

namespace TicketingSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly JwtService _jwt;

        public AuthService(IUserRepository repo, JwtService jwt)
        {
            _repo = repo;
            _jwt = jwt;
        }

        public string Register(RegisterDto dto)
        {
            if (_repo.GetByEmail(dto.Email) != null)
                throw new Exception("Email already exists");

            var user = new User
            {
                Email = dto.Email,
                FullName = dto.FullName,
                PasswordHash = PasswordHelper.Hash(dto.Password),
                Role = "User"
            };

            _repo.Add(user);
            _repo.Save();

            return "User registered successfully";
        }

        public AuthResponseDto Login(LoginDto dto)
        {
            var user = _repo.GetByEmail(dto.Email);

            if (user == null)
                throw new Exception("User not found");

            if (!PasswordHelper.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Invalid password");

            var token = _jwt.GenerateToken(user.Email, user.Role);

            return new AuthResponseDto
            {
                Message = "Login successful",
                Token = token
            };
        }
    }
}