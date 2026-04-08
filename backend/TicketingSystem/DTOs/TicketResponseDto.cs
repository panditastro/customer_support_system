// DTOs/TicketResponseDto.cs
namespace TicketingSystem.DTOs;

public class TicketResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // Status aur Priority string me map karenge
    public string Status { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}