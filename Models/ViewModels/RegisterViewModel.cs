using System.ComponentModel.DataAnnotations;

namespace TicketTrackingSystem.Models.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords do not match")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
    
    [Required(ErrorMessage = "Role is required")]
    public string Role { get; set; }
}