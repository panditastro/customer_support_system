namespace TicketingSystem.Models;

public class Ticket
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;        // FIX warning
    public string Description { get; set; } = string.Empty;  // FIX warning

    public string Status { get; set; } = "Open";

    public int UserId { get; set; }
}