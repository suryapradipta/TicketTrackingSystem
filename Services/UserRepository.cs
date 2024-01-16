using MongoDB.Driver;
using TicketTrackingSystem.Models;
using TicketTrackingSystem.Services.Interfaces;

namespace TicketTrackingSystem.Services;

public class UserRepository : IUserRepository
{
    private readonly IMongoDatabase _database;

    public UserRepository(IMongoDBContext dbContext)
    {
        _database = dbContext.Database;
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        var usersCollection = _database.GetCollection<User>("Users");
        return await usersCollection.Find(u => u.Username == username).FirstOrDefaultAsync();
    }
    
    public async Task CreateUserAsync(User user)
    {
        var usersCollection = _database.GetCollection<User>("Users");
        await usersCollection.InsertOneAsync(user);
    }
}