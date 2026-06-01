using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Services;

namespace web.Controllers;

public class CartController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly CartService _cart;

    public CartController(ApplicationDbContext db, CartService cart)
    {
        _db = db;
        _cart = cart;
    }

    public IActionResult Index()
    {
        return View(_cart.GetItems());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int productId, int? sizeId, List<int>? toppingIds, int quantity)
    {
        var product = await _db.Products
            .FirstOrDefaultAsync(p => p.Id == productId && p.IsAvailable);
        if (product is null)
            return NotFound();

        quantity = Math.Clamp(quantity, 1, 20);

        var unitPrice = product.BasePrice;
        int? appliedSizeId = null;
        string? sizeName = null;
        var toppingNames = new List<string>();
        var appliedToppingIds = new List<int>();

        // Size and toppings only apply to customizable products (pizzas).
        if (product.IsCustomizable)
        {
            if (sizeId is not null)
            {
                var size = await _db.PizzaSizes.FindAsync(sizeId.Value);
                if (size is not null)
                {
                    unitPrice += size.PriceModifier;
                    appliedSizeId = size.Id;
                    sizeName = size.Name;
                }
            }

            if (toppingIds is { Count: > 0 })
            {
                var toppings = await _db.Toppings
                    .Where(t => toppingIds.Contains(t.Id))
                    .OrderBy(t => t.DisplayOrder)
                    .ToListAsync();
                foreach (var t in toppings)
                {
                    unitPrice += t.Price;
                    toppingNames.Add(t.Name);
                    appliedToppingIds.Add(t.Id);
                }
            }
        }

        var key = $"{productId}-{appliedSizeId?.ToString() ?? "0"}-" +
                  string.Join("_", appliedToppingIds.OrderBy(x => x));

        _cart.Add(new CartItem
        {
            Key = key,
            ProductId = product.Id,
            ProductName = product.Name,
            ImageUrl = product.ImageUrl,
            SizeId = appliedSizeId,
            SizeName = sizeName,
            ToppingsSummary = string.Join(", ", toppingNames),
            UnitPrice = unitPrice,
            Quantity = quantity
        });

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(string key)
    {
        _cart.Remove(key);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ChangeQuantity(string key, int delta)
    {
        _cart.ChangeQuantity(key, delta);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Clear()
    {
        _cart.Clear();
        return RedirectToAction(nameof(Index));
    }
}
