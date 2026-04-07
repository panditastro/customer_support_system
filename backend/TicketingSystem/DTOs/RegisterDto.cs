using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.DTOs
{
    /// <summary>
    /// Register request DTO
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// User email
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// User password
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Confirm password
        /// </summary>
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        /// <summary>
        /// Full name
        /// </summary>
        [MaxLength(100)]
        public string? FullName { get; set; }
    }
}