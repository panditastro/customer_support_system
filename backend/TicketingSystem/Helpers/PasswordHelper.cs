using BCrypt.Net;

namespace TicketingSystem.Helpers
{
    /// <summary>
    /// Helper class for hashing and verifying passwords using BCrypt.
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>
        /// Hashes a plain text password.
        /// </summary>
        /// <param name="password">Plain text password</param>
        /// <returns>Hashed password</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty");

            // 🔐 Work factor (12 = secure default)
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        /// <summary>
        /// Verifies a password against a hashed value.
        /// </summary>
        /// <param name="password">Plain text password</param>
        /// <param name="hash">Stored hashed password</param>
        /// <returns>True if valid, otherwise false</returns>
        public static bool VerifyPassword(string password, string hash)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hash))
                return false;

            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}