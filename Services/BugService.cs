using System.Security.Claims;
using TicketTrackingSystem.Models;
using TicketTrackingSystem.Services.Interfaces;

namespace TicketTrackingSystem.Services;

public class BugService : IBugService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBugRepository _bugRepository;

    public BugService(IHttpContextAccessor httpContextAccessor, IBugRepository bugRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _bugRepository = bugRepository;
    }

    public async Task<IEnumerable<Bug>> GetBugsAsync()
    {
        return await _bugRepository.GetBugsAsync();
    }

    public async Task<Bug> GetBugByIdAsync(string bugId)
    {
        return await _bugRepository.GetBugByIdAsync(bugId);
    }

    public async Task CreateBugAsync(Bug bug, string userId)
    {
        if (!IsUserInRole(UserRole.QA))
        {
            throw new UnauthorizedAccessException("Only QA can create a bug.");
        }

        if (string.IsNullOrEmpty(bug.Summary) || string.IsNullOrEmpty(bug.Description))
        {
            throw new InvalidOperationException("Summary and Description are required.");
        }

        bug.Status = BugStatus.Open;
        await _bugRepository.CreateBugAsync(bug);
    }

    public async Task UpdateBugAsync(Bug bug, string userId)
    {
        if (!IsUserInRole(UserRole.QA))
        {
            throw new UnauthorizedAccessException("Only QA can edit a bug.");
        }

        await _bugRepository.UpdateBugAsync(bug);
    }

    public async Task DeleteBugAsync(string bugId, string userId)
    {
        if (!IsUserInRole(UserRole.QA))
        {
            throw new UnauthorizedAccessException("Only QA can delete a bug.");
        }

        await _bugRepository.DeleteBugAsync(bugId);
    }

    private bool IsUserInRole(UserRole role)
    {
        var user = _httpContextAccessor.HttpContext.User;
        return user.HasClaim(ClaimTypes.Role, role.ToString());
    }
    
    public async Task ResolveBugAsync(string bugId, string userId)
    {
        if (!IsUserInRole(UserRole.RD))
        {
            throw new UnauthorizedAccessException("Only RD can resolve a bug.");
        }

        var bug = await _bugRepository.GetBugByIdAsync(bugId);

        if (bug == null)
        {
            throw new InvalidOperationException("Bug not found.");
        }

        bug.Status = BugStatus.Resolved;
        await _bugRepository.UpdateBugAsync(bug);
    }
}

public enum UserRole
{
    QA,
    RD
}