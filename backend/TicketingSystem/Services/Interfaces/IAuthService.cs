using TicketingSystem.DTOs;

namespace TicketingSystem.Services.Interfaces
{
    public interface IAuthService
    {
        string Register(RegisterDto dto);
        AuthResponseDto Login(LoginDto dto);
    }
}