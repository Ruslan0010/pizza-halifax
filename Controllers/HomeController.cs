using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;

    public HomeController(ApplicationDbContext db)
    {
        _db = db;
    }

    // Storefront: the menu grouped by category.
    public async Task<IActionResult> Index()
    {
        var categories = await _db.Categories
            .OrderBy(c => c.DisplayOrder)
            .Include(c => c.Products.Where(p => p.IsAvailable).OrderBy(p => p.Id))
            .Where(c => c.Products.Any(p => p.IsAvailable))
            .ToListAsync();

        return View(categories);
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
