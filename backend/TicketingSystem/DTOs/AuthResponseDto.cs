namespace TicketingSystem.DTOs
{
    /// <summary>
    /// Authentication response DTO
    /// </summary>
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}