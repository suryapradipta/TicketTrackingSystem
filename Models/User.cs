using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TicketTrackingSystem.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Role is required")]
    public string Role { get; set; }
}