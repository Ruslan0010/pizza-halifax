using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using web.ViewModels;

namespace web.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;

    public HomeController(ApplicationDbContext db)
    {
        _db = db;
    }

    // Storefront: the menu grouped by category, with optional search and filter.
    public async Task<IActionResult> Index(string? q, int? category)
    {
        var allCategories = await _db.Categories.OrderBy(c => c.DisplayOrder).ToListAsync();

        var query = _db.Products.Where(p => p.IsAvailable);
        if (category is not null)
            query = query.Where(p => p.CategoryId == category);
        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = $"%{q.Trim()}%";
            query = query.Where(p => EF.Functions.ILike(p.Name, term) || EF.Functions.ILike(p.Description, term));
        }

        var products = await query.OrderBy(p => p.Id).ToListAsync();

        var categories = allCategories
            .Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name,
                DisplayOrder = c.DisplayOrder,
                Products = products.Where(p => p.CategoryId == c.Id).ToList()
            })
            .Where(c => c.Products.Count > 0)
            .ToList();

        var vm = new MenuViewModel
        {
            Categories = categories,
            AllCategories = allCategories,
            Query = q,
            SelectedCategoryId = category,
            ResultCount = products.Count
        };
        return View(vm);
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
