using TicketingSystem.Models;
using TicketingSystem.DTOs;
using TicketingSystem.Interfaces;

namespace TicketingSystem.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _repo;

    public TicketService(ITicketRepository repo)
    {
        _repo = repo;
    }

    public async Task<Ticket> CreateTicketAsync(TicketDto dto)
    {
        var ticket = new Ticket
        {
            Title = dto.Title,
            Description = dto.Description,
            UserId = dto.UserId
        };

        return await _repo.AddAsync(ticket);
    }

    public async Task<List<Ticket>> GetAllTicketsAsync()
    {
        return await _repo.GetAllAsync();
    }
}