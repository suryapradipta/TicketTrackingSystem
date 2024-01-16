using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TicketTrackingSystem.Models;
using TicketTrackingSystem.Models.ViewModels;

namespace TicketTrackingSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var username = User.Identity?.Name;

        var viewModel = new IndexViewModel
        {
            Username = username,
            Role = User.FindFirst(ClaimTypes.Role)?.Value,
        };
        
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
