using TicketTrackingSystem.Models;
using TicketTrackingSystem.Services.Interfaces;

namespace TicketTrackingSystem.Services;

using MongoDB.Driver;

public class BugRepository : IBugRepository
{
    private readonly IMongoDatabase _database;

    public BugRepository(IMongoDBContext dbContext)
    {
        _database = dbContext.Database;
    }

    public async Task<IEnumerable<Bug>> GetBugsAsync()
    {
        var bugsCollection = _database.GetCollection<Bug>("Bugs");
        return await bugsCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Bug> GetBugByIdAsync(string bugId)
    {
        var bugsCollection = _database.GetCollection<Bug>("Bugs");
        return await bugsCollection.Find(b => b.Id == bugId).FirstOrDefaultAsync();
    }

    public async Task CreateBugAsync(Bug bug)
    {
        var bugsCollection = _database.GetCollection<Bug>("Bugs");
        await bugsCollection.InsertOneAsync(bug);
    }

    public async Task UpdateBugAsync(Bug bug)
    {
        var bugsCollection = _database.GetCollection<Bug>("Bugs");
        await bugsCollection.ReplaceOneAsync(b => b.Id == bug.Id, bug);
    }

    public async Task DeleteBugAsync(string bugId)
    {
        var bugsCollection = _database.GetCollection<Bug>("Bugs");
        await bugsCollection.DeleteOneAsync(b => b.Id == bugId);
    }
}
