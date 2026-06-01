using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using web.ViewModels;

namespace web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _db;

    public AdminController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.ProductCount = await _db.Products.CountAsync();
        ViewBag.OrderCount = await _db.Orders.CountAsync();
        ViewBag.Revenue = await _db.Orders.SumAsync(o => (decimal?)o.Total) ?? 0m;
        return View();
    }

    // ---------- Products ----------

    public async Task<IActionResult> Products()
    {
        var products = await _db.Products
            .Include(p => p.Category)
            .OrderBy(p => p.CategoryId).ThenBy(p => p.Name)
            .ToListAsync();
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        return View("ProductForm", new ProductFormViewModel { Categories = await CategoryOptions() });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProduct(ProductFormViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            vm.Categories = await CategoryOptions();
            return View("ProductForm", vm);
        }

        _db.Products.Add(new Product
        {
            Name = vm.Name,
            Description = vm.Description,
            ImageUrl = vm.ImageUrl,
            BasePrice = vm.BasePrice,
            CategoryId = vm.CategoryId,
            IsAvailable = vm.IsAvailable,
            IsCustomizable = vm.IsCustomizable
        });
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Products));
    }

    [HttpGet]
    public async Task<IActionResult> EditProduct(int id)
    {
        var p = await _db.Products.FindAsync(id);
        if (p is null)
            return NotFound();

        var vm = new ProductFormViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            ImageUrl = p.ImageUrl,
            BasePrice = p.BasePrice,
            CategoryId = p.CategoryId,
            IsAvailable = p.IsAvailable,
            IsCustomizable = p.IsCustomizable,
            Categories = await CategoryOptions()
        };
        return View("ProductForm", vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProduct(ProductFormViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            vm.Categories = await CategoryOptions();
            return View("ProductForm", vm);
        }

        var p = await _db.Products.FindAsync(vm.Id);
        if (p is null)
            return NotFound();

        p.Name = vm.Name;
        p.Description = vm.Description;
        p.ImageUrl = vm.ImageUrl;
        p.BasePrice = vm.BasePrice;
        p.CategoryId = vm.CategoryId;
        p.IsAvailable = vm.IsAvailable;
        p.IsCustomizable = vm.IsCustomizable;
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Products));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var p = await _db.Products.FindAsync(id);
        if (p is not null)
        {
            _db.Products.Remove(p);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Products));
    }

    // ---------- Orders ----------

    public async Task<IActionResult> Orders()
    {
        var orders = await _db.Orders
            .Include(o => o.Items)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
        return View(orders);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateOrderStatus(int id, OrderStatus status)
    {
        var order = await _db.Orders.FindAsync(id);
        if (order is not null)
        {
            order.Status = status;
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Orders));
    }

    private async Task<List<SelectListItem>> CategoryOptions() =>
        await _db.Categories
            .OrderBy(c => c.DisplayOrder)
            .Select(c => new SelectListItem(c.Name, c.Id.ToString()))
            .ToListAsync();
}
