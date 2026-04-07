using TicketingSystem.Models;

namespace TicketingSystem.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for managing user data
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Get user by email
        /// </summary>
        Task<User?> GetByEmailAsync(string email);

        /// <summary>
        /// Get user by id
        /// </summary>
        Task<User?> GetByIdAsync(int id);

        /// <summary>
        /// Add new user
        /// </summary>
        Task AddAsync(User user);

        /// <summary>
        /// Update existing user
        /// </summary>
        Task UpdateAsync(User user);

        /// <summary>
        /// Check if email already exists
        /// </summary>
        Task<bool> ExistsByEmailAsync(string email);
    }
}