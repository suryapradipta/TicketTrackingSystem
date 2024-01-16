using System.Security.Claims;
using TicketTrackingSystem.Services.Interfaces;

namespace TicketTrackingSystem.Controllers;

using Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class BugsController : Controller
{
    private readonly IBugService _bugService;

    public BugsController(IBugService bugService)
    {
        _bugService = bugService;
    }

    public async Task<IActionResult> Index()
    {
        var bugs = await _bugService.GetBugsAsync();
        return View(bugs);
    }

    [Authorize(Roles = "QA")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "QA")]
    public async Task<IActionResult> Create(Bug bug)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _bugService.CreateBugAsync(bug, userId);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(bug);
        }
    }

    [Authorize(Roles = "QA")]
    public async Task<IActionResult> Edit(string id)
    {
        var bug = await _bugService.GetBugByIdAsync(id);

        if (bug == null)
        {
            return NotFound();
        }

        return View(bug);
    }

    [HttpPost]
    [Authorize(Roles = "QA")]
    public async Task<IActionResult> Edit(string id, Bug bug)
    {
        if (id != bug.Id)
        {
            return NotFound();
        }

        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _bugService.UpdateBugAsync(bug, userId);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(bug);
        }
    }

    [Authorize(Roles = "QA")]
    public async Task<IActionResult> Delete(string id)
    {
        var bug = await _bugService.GetBugByIdAsync(id);

        if (bug == null)
        {
            return NotFound();
        }

        return View(bug);
    }

    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "QA")]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _bugService.DeleteBugAsync(id, userId);
        return RedirectToAction(nameof(Index));
    }
    
    
    [Authorize(Roles = "RD")]
    public async Task<IActionResult> Resolve(string id)
    {
        var bug = await _bugService.GetBugByIdAsync(id);

        if (bug == null)
        {
            return NotFound();
        }

        return View(bug);
    }

    [HttpPost, ActionName("Resolve")]
    [Authorize(Roles = "RD")]
    public async Task<IActionResult> ResolveConfirmed(string id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _bugService.ResolveBugAsync(id, userId);
        return RedirectToAction(nameof(Index));
    }
}

