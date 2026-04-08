// Repositories/TicketCommentRepository.cs
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Data;
using TicketingSystem.Interfaces;
using TicketingSystem.Models;

namespace TicketingSystem.Repositories;

public class TicketCommentRepository : ITicketCommentRepository
{
    private readonly AppDbContext _context;
    public TicketCommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TicketComment> AddAsync(TicketComment comment)
    {
        _context.TicketComments.Add(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<List<TicketComment>> GetByTicketIdAsync(int ticketId)
    {
        return await _context.TicketComments
            .Include(c => c.User)
            .Where(c => c.TicketId == ticketId)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync();
    }
}