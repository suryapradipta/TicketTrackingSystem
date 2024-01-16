using TicketTrackingSystem.Models;

namespace TicketTrackingSystem.Services.Interfaces;

public interface IBugService
{
    Task<IEnumerable<Bug>> GetBugsAsync();
    Task<Bug> GetBugByIdAsync(string bugId);
    Task CreateBugAsync(Bug bug, string userId);
    Task UpdateBugAsync(Bug bug, string userId);
    Task DeleteBugAsync(string bugId, string userId);
    Task ResolveBugAsync(string bugId, string userId);
}