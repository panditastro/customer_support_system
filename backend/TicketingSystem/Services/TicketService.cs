using TicketingSystem.Models;
using TicketingSystem.DTOs;
using TicketingSystem.Interfaces;
using TicketingSystem.Repositories.Interfaces;

namespace TicketingSystem.Services;

public class TicketService : ITicketService
{
 private readonly ITicketRepository _ticketRepo;
    private readonly IUserRepository _userRepo;

    public TicketService(ITicketRepository ticketRepo, IUserRepository userRepo)
    {
        _ticketRepo = ticketRepo;
        _userRepo = userRepo;
    }


    public async Task<Ticket> CreateTicketAsync(int userId, TicketDto dto)
    {
        var ticket = new Ticket
        {
            Title = dto.Title,
            Description = dto.Description,
            UserId = userId,
            Status = TicketStatus.Open,
            Priority = dto.Priority,
            CreatedAt = DateTime.UtcNow
        };

        return await _ticketRepo.CreateAsync(ticket);
    }


     public async Task<IEnumerable<TicketResponseDto>> GetMyTicketsAsync(int userId)
    {
        // Validate user
        var user = await _userRepo.GetByIdAsync(userId);
        if (user == null || !user.IsActive)
            throw new UnauthorizedAccessException("User not found or inactive");

        var tickets = await _ticketRepo.GetTicketsByUserIdAsync(userId);

        return tickets.Select(t => new TicketResponseDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Status = t.Status.ToString(),
            Priority = t.Priority.ToString(),
            CreatedAt = t.CreatedAt
        });
    }
}