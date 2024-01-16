using TicketTrackingSystem.Models;

namespace TicketTrackingSystem.Services.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserByUsernameAsync(string username);
    Task CreateUserAsync(User user);
}