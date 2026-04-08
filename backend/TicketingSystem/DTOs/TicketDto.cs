using System.ComponentModel.DataAnnotations;
using TicketingSystem.Models;

namespace TicketingSystem.DTOs;

public class TicketDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    public TicketPriority Priority { get; set; } = TicketPriority.Medium;
}