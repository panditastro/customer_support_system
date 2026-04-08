using TicketingSystem.DTOs;
using TicketingSystem.Helpers;
using TicketingSystem.Models;
using TicketingSystem.Repositories.Interfaces;
using TicketingSystem.Services.Interfaces;
using System.Security.Claims;

namespace TicketingSystem.Services
{
    /// <summary>
    /// Handles authentication (Register + Login)
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly JwtService _jwt;
        
        public AuthService(IUserRepository repo, JwtService jwt)
        {
            _repo = repo;
            _jwt = jwt;
        }


        public int GetUserId(ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)
                              ?? user.FindFirst("id");

            if (userIdClaim == null)
                throw new UnauthorizedAccessException("User ID not found in token");

            return int.Parse(userIdClaim.Value);
        }

        /// <summary>
        /// Register new user
        /// </summary>
        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            // ✅ Check duplicate email
            var exists = await _repo.ExistsByEmailAsync(dto.Email);
            if (exists)
                throw new Exception("Email already exists");

            // ✅ Create user
            var user = new User
            {
                Email = dto.Email,
                FullName = dto.FullName,
                PasswordHash = PasswordHelper.HashPassword(dto.Password),

                // 🔐 Always default role (prevent hacking)
                Role = UserRole.User,

                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _repo.AddAsync(user);

            // ✅ Generate token after register
            var token = _jwt.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }

        /// <summary>
        /// Login user
        /// </summary>
        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _repo.GetByEmailAsync(dto.Email);

            // ❌ User not found
            if (user == null)
                throw new Exception("User not found");

            // ❌ Inactive user
            if (!user.IsActive)
                throw new Exception("User is inactive");

            // ❌ Wrong password
            var isValid = PasswordHelper.VerifyPassword(dto.Password, user.PasswordHash);
            if (!isValid)
                throw new Exception("Invalid password");

            // ✅ Update last login
            user.LastLoginAt = DateTime.UtcNow;
            await _repo.UpdateAsync(user);

            // ✅ Generate JWT
            var token = _jwt.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }

      public async Task<string> CreateAgentAsync(CreateUserDto dto)
        {
            if (await _repo.ExistsByEmailAsync(dto.Email))
                throw new Exception("Email already exists");

            var user = new User
            {
                Email = dto.Email,
                FullName = dto.FullName ?? "",
                PasswordHash = PasswordHelper.HashPassword(dto.Password),
                Role = UserRole.Agent
            };

            await _repo.AddAsync(user);

            return "Agent created successfully";
        }

        public async Task<string> CreateAdminAsync(CreateUserDto dto)
        {
            if (await _repo.ExistsByEmailAsync(dto.Email))
                throw new Exception("Email already exists");

            var user = new User
            {
                Email = dto.Email,
                FullName = dto.FullName ?? "",
                PasswordHash = PasswordHelper.HashPassword(dto.Password),
                Role = UserRole.Admin
            };

            await _repo.AddAsync(user);

            return "Admin created successfully";
        }
    }
}