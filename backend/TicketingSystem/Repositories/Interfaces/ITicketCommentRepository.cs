// Repositories/Interfaces/ITicketCommentRepository.cs
using TicketingSystem.Models;

namespace TicketingSystem.Interfaces;

public interface ITicketCommentRepository
{
    Task<TicketComment> AddAsync(TicketComment comment);
    Task<List<TicketComment>> GetByTicketIdAsync(int ticketId);
}