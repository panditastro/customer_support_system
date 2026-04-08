using TicketingSystem.DTOs;
using TicketingSystem.Models;

namespace TicketingSystem.Interfaces;

public interface ITicketService
{
    Task<Ticket> CreateTicketAsync(int userId, TicketDto dto);

    Task<IEnumerable<TicketResponseDto>> GetMyTicketsAsync(int userId);
}