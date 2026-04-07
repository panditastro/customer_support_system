using TicketingSystem.DTOs;

namespace TicketingSystem.Services.Interfaces
{
    /// <summary>
    /// Handles authentication operations
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Register new user
        /// </summary>
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);

        /// <summary>
        /// Login user and return JWT token
        /// </summary>
        Task<AuthResponseDto> LoginAsync(LoginDto dto);


        Task<string> CreateAgentAsync(CreateUserDto dto);
        Task<string> CreateAdminAsync(CreateUserDto dto);
    }
}