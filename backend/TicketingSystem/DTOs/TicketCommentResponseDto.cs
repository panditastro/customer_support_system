namespace TicketingSystem.DTOs;

public class TicketCommentResponseDto
{
    public int Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}