using TicketingSystem.Models;

namespace TicketingSystem.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User? GetByEmail(string email);
        void Add(User user);
        void Save();
    }
}