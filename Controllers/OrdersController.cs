using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;

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
}
