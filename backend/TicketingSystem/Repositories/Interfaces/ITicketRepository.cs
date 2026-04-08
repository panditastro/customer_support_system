using TicketingSystem.Models;

namespace TicketingSystem.Interfaces;

public interface ITicketRepository
{
    Task<Ticket> CreateAsync(Ticket ticket);
    Task<Ticket> GetByIdAsync(int id);
    Task<List<Ticket>> GetTicketsByUserIdAsync(int userId);
    Task<List<Ticket>> GetAllAsync();
}