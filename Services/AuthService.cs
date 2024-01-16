using TicketTrackingSystem.Models;
using TicketTrackingSystem.Models.ViewModels;
using TicketTrackingSystem.Services.Interfaces;

namespace TicketTrackingSystem.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);

        if (user != null && user.Password == password)
        {
            return user;
        }
        

        return null;
    }
    
    public async Task<bool> RegisterAsync(RegisterViewModel model)
    {
        var existingUser = await _userRepository.GetUserByUsernameAsync(model.Username);
        if (existingUser != null)
        {
            return false;
        }

        var newUser = new User
        {
            Username = model.Username,
            Password = model.Password,
            Role = model.Role,
        };

        await _userRepository.CreateUserAsync(newUser);

        return true; 
    }
}