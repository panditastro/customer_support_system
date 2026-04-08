// Services/Interfaces/ITicketCommentService.cs
using TicketingSystem.DTOs;

namespace TicketingSystem.Interfaces;

public interface ITicketCommentService
{
    Task<TicketCommentResponseDto> AddCommentAsync(int ticketId, int userId, TicketCommentDto dto);
    Task<List<TicketCommentResponseDto>> GetCommentsAsync(int ticketId, int userId);
}