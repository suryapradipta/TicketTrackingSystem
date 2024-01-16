using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TicketTrackingSystem.Models;

public class Bug
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [Required(ErrorMessage = "Summary is required")]
    public string Summary { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Status is required")]
    public BugStatus Status { get; set; }
}

public enum BugStatus
{
    Open,
    InProgress,
    Resolved
}