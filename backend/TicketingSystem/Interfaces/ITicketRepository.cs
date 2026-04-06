using TicketingSystem.Models;

namespace TicketingSystem.Interfaces;

public interface ITicketRepository
{
    Task<Ticket> AddAsync(Ticket ticket);
    Task<List<Ticket>> GetAllAsync();
}