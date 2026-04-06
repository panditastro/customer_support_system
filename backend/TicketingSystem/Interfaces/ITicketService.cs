using TicketingSystem.Models;
using TicketingSystem.DTOs;

namespace TicketingSystem.Interfaces;

public interface ITicketService
{
    Task<Ticket> CreateTicketAsync(TicketDto dto);
    Task<List<Ticket>> GetAllTicketsAsync();
}