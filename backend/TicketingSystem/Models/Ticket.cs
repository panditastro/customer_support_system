namespace TicketingSystem.Models;

public class Ticket
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public TicketStatus? Status { get; set; } = TicketStatus.Open;

    public int UserId { get; set; }           // Customer

    public int? AssignedAgentId { get; set; } // Agent (future use)

    public TicketPriority? Priority { get; set; } = TicketPriority.Medium; // ✅ enum

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ClosedAt { get; set; }
}