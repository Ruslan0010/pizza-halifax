using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private readonly ApplicationDbContext _db;

    public OrdersController(ApplicationDbContext db)
    {
        _db = db;
    }

    // The signed-in customer's own order history.
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var orders = await _db.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.Items)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        return View(orders);
    }

    // Public order tracking by order number. No login required so a customer
    // can check status from the confirmation page or a shared link.
    [AllowAnonymous]
    public async Task<IActionResult> Track(string? orderNumber)
    {
        if (string.IsNullOrWhiteSpace(orderNumber))
            return View(model: (Order?)null);

        var order = await _db.Orders
            .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber.Trim());

        ViewData["SearchedNumber"] = orderNumber.Trim();
        return View(order);
    }
}
