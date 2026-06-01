using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using web.Services;
using web.ViewModels;

namespace web.Controllers;

public class CheckoutController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly CartService _cart;

    public CheckoutController(ApplicationDbContext db, CartService cart)
    {
        _db = db;
        _cart = cart;
    }

    public IActionResult Index()
    {
        if (_cart.GetItems().Count == 0)
            return RedirectToAction("Index", "Cart");

        return View(new CheckoutViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PlaceOrder(CheckoutViewModel model)
    {
        var items = _cart.GetItems();
        if (items.Count == 0)
            return RedirectToAction("Index", "Cart");

        var isPickup = model.OrderType == "Pickup";

        // Address is only required for delivery.
        if (!isPickup && string.IsNullOrWhiteSpace(model.DeliveryAddress))
            ModelState.AddModelError(nameof(model.DeliveryAddress), "Please enter a delivery address.");

        if (!ModelState.IsValid)
            return View("Index", model);

        // Pickup orders have no delivery fee.
        var deliveryFee = isPickup ? 0m : _cart.DeliveryCost();
        var subTotal = _cart.SubTotal();
        var discount = _cart.Discount();

        // Payment is simulated: we record the order as Confirmed without a gateway.
        var order = new Order
        {
            OrderNumber = $"PH-{DateTime.UtcNow:yyMMdd}-{Random.Shared.Next(1000, 9999)}",
            CustomerName = model.CustomerName,
            CustomerPhone = model.CustomerPhone,
            DeliveryAddress = isPickup ? "Pickup at store" : model.DeliveryAddress!,
            Notes = model.Notes,
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            OrderType = isPickup ? OrderType.Pickup : OrderType.Delivery,
            Status = OrderStatus.Confirmed,
            SubTotal = subTotal,
            DiscountAmount = discount,
            PromoCodeUsed = _cart.PromoCode(),
            DeliveryFee = deliveryFee,
            Total = subTotal - discount + deliveryFee,
            CreatedAt = DateTime.UtcNow,
            Items = items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                SizeName = i.SizeName,
                ToppingsSummary = i.ToppingsSummary,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity,
                LineTotal = i.LineTotal
            }).ToList()
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();
        _cart.Clear();

        return RedirectToAction(nameof(Confirmation), new { orderNumber = order.OrderNumber });
    }

    public async Task<IActionResult> Confirmation(string orderNumber)
    {
        var order = await _db.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);

        if (order is null)
            return NotFound();

        return View(order);
    }
}
