using TicketingSystem.Models;
using TicketingSystem.Interfaces;
using TicketingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace TicketingSystem.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly AppDbContext _context;

    public TicketRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Ticket> CreateAsync(Ticket ticket)
    {
        await _context.Tickets.AddAsync(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task<Ticket> GetByIdAsync(int id)
    {
        // Always awaited properly
        return await _context.Tickets
                             .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<Ticket>> GetTicketsByUserIdAsync(int userId)
    {
        return await _context.Tickets
                             .Where(t => t.UserId == userId)
                             .OrderByDescending(t => t.CreatedAt)
                             .ToListAsync();
    }

    public async Task<List<Ticket>> GetAllAsync()
    {
        return await _context.Tickets
                             .OrderByDescending(t => t.CreatedAt)
                             .ToListAsync();
    }
}