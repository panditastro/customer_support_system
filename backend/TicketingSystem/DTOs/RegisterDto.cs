using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.DTOs
{
    public class RegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(4)]
        public string Password { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;
    }
}