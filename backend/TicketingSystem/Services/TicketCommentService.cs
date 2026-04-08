// Services/TicketCommentService.cs
using TicketingSystem.DTOs;
using TicketingSystem.Interfaces;
using TicketingSystem.Models;
using TicketingSystem.Repositories.Interfaces;

namespace TicketingSystem.Services;

public class TicketCommentService : ITicketCommentService
{
    private readonly ITicketCommentRepository _repo;
    private readonly ITicketRepository _ticketRepo;
    private readonly IUserRepository _userRepo;

    public TicketCommentService(
        ITicketCommentRepository repo,
        ITicketRepository ticketRepo,
        IUserRepository userRepo)
    {
        _repo = repo;
        _ticketRepo = ticketRepo;
        _userRepo = userRepo;
    }

    public async Task<TicketCommentResponseDto> AddCommentAsync(int ticketId, int userId, TicketCommentDto dto)
    {
        var ticket = await _ticketRepo.GetByIdAsync(ticketId);
        if (ticket == null) throw new KeyNotFoundException("Ticket not found");
        if (ticket.UserId != userId) throw new UnauthorizedAccessException("Cannot comment on this ticket");

        var comment = new TicketComment
        {
            TicketId = ticketId,
            UserId = userId,
            Message = dto.Message,
        };

        var savedComment = await _repo.AddAsync(comment);

        var user = await _userRepo.GetByIdAsync(userId);

        return new TicketCommentResponseDto
        {
            Id = savedComment.Id,
            Message = savedComment.Message,
            UserName = user?.FullName ?? user?.Email ?? "Unknown",
            Role = user?.Role.ToString() ?? "User",
            CreatedAt = savedComment.CreatedAt
        };
    }

    public async Task<List<TicketCommentResponseDto>> GetCommentsAsync(int ticketId, int userId)
    {
        var ticket = await _ticketRepo.GetByIdAsync(ticketId);
        if (ticket == null) throw new KeyNotFoundException("Ticket not found");
        if (ticket.UserId != userId) throw new UnauthorizedAccessException("Cannot view comments");

        var comments = await _repo.GetByTicketIdAsync(ticketId);

        return comments.Select(c => new TicketCommentResponseDto
        {
            Id = c.Id,
            Message = c.Message,
            UserName = c.User.FullName ?? c.User.Email,
            Role = c.User.Role.ToString(),
            CreatedAt = c.CreatedAt
        }).ToList();
    }
}