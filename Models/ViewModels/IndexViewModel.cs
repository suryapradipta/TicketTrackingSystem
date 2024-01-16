using System.ComponentModel.DataAnnotations;

namespace TicketTrackingSystem.Models.ViewModels;

public class IndexViewModel
{
    [Required(ErrorMessage = "Username is required")]
    public string? Username { get; set; }
    
    [Required(ErrorMessage = "Role is required")]
    public string? Role { get; set; }
}