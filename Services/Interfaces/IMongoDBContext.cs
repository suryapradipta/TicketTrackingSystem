using MongoDB.Driver;
using TicketTrackingSystem.Models;

namespace TicketTrackingSystem.Services.Interfaces;

public interface IMongoDBContext
{
    IMongoCollection<Bug> Bugs { get; }
    IMongoCollection<User> Users { get; }
    IMongoDatabase Database { get; } 
}