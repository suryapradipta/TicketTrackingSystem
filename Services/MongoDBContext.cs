using MongoDB.Driver;
using TicketTrackingSystem.Models;
using TicketTrackingSystem.Services.Interfaces;

namespace TicketTrackingSystem.Services;

public class MongoDBContext : IMongoDBContext
{
    private readonly IMongoDatabase _database;

    public MongoDBContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
        _database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
    }

    public IMongoCollection<Bug> Bugs => _database.GetCollection<Bug>("Bugs");
    public IMongoDatabase Database => _database;
    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
}