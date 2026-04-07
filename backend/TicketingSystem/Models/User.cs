using System.ComponentModel.DataAnnotations;


namespace TicketingSystem.Models
{
    /// <summary>
    /// Represents an application user with authentication and profile details.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User email (unique)
        /// </summary>
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Hashed password
        /// </summary>
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Full name of user
        /// </summary>
        [MaxLength(100)]
        public string? FullName { get; set; }

        /// <summary>
        /// User role (User, SupportAgent, Admin)
        /// </summary>
        [Required]
        public UserRole Role { get; set; } = UserRole.User;

        /// <summary>
        /// User active status
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Email verification status
        /// </summary>
        public bool IsEmailVerified { get; set; } = false;

        /// <summary>
        /// User phone number
        /// </summary>
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Profile image URL
        /// </summary>
        [MaxLength(255)]
        public string? ProfileImageUrl { get; set; }

        /// <summary>
        /// Record creation time (UTC)
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Last update time
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Last login timestamp
        /// </summary>
        public DateTime? LastLoginAt { get; set; }

        /// <summary>
        /// Refresh token (for JWT refresh flow)
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Refresh token expiry time
        /// </summary>
        public DateTime? RefreshTokenExpiry { get; set; }
    }
}