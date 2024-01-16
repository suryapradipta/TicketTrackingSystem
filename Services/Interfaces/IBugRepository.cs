using TicketTrackingSystem.Models;

namespace TicketTrackingSystem.Services.Interfaces;

public interface IBugRepository
{
    Task<IEnumerable<Bug>> GetBugsAsync();
    Task<Bug> GetBugByIdAsync(string bugId);
    Task CreateBugAsync(Bug bug);
    Task UpdateBugAsync(Bug bug);
    Task DeleteBugAsync(string bugId);
}