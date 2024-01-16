using TicketTrackingSystem.Models.ViewModels;
using TicketTrackingSystem.Services.Interfaces;

namespace TicketTrackingSystem.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Models;

public class AccountController : Controller
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _authService.AuthenticateAsync(model.Username, model.Password);

            if (user != null)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home"); // Redirect to a secure area
            }

            ModelState.AddModelError("", "Invalid username or password");
        }

        return View(model);
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var isRegistered = await _authService.RegisterAsync(model);

            if (isRegistered)
            {
                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("", "Username is already taken");
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }
}