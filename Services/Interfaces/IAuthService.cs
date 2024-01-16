using TicketTrackingSystem.Models;
using TicketTrackingSystem.Models.ViewModels;

namespace TicketTrackingSystem.Services.Interfaces;

public interface IAuthService
{
    Task<User?> AuthenticateAsync(string username, string password);
    Task<bool> RegisterAsync(RegisterViewModel model);
}