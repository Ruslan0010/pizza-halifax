using System.Text.Json;

namespace web.Services;

// Stores the cart in the user's session as JSON. Kept simple on purpose:
// the cart only needs to survive a browsing session, not be persisted.
public class CartService
{
    private const string SessionKey = "Cart";
    private const decimal DeliveryFee = 4.99m;
    private const decimal FreeDeliveryThreshold = 25.00m;

    private readonly IHttpContextAccessor _accessor;

    public CartService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    private ISession Session => _accessor.HttpContext!.Session;

    public List<CartItem> GetItems()
    {
        var json = Session.GetString(SessionKey);
        return string.IsNullOrEmpty(json)
            ? new List<CartItem>()
            : JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>();
    }

    private void Save(List<CartItem> items) =>
        Session.SetString(SessionKey, JsonSerializer.Serialize(items));

    public void Add(CartItem item)
    {
        var items = GetItems();
        var existing = items.FirstOrDefault(i => i.Key == item.Key);
        if (existing is null)
            items.Add(item);
        else
            existing.Quantity += item.Quantity;
        Save(items);
    }

    public void Remove(string key)
    {
        var items = GetItems();
        items.RemoveAll(i => i.Key == key);
        Save(items);
    }

    // Change a line's quantity by delta (+1 / -1). Removes the line if it drops to 0.
    public void ChangeQuantity(string key, int delta)
    {
        var items = GetItems();
        var item = items.FirstOrDefault(i => i.Key == key);
        if (item is null)
            return;

        item.Quantity += delta;
        if (item.Quantity <= 0)
            items.RemoveAll(i => i.Key == key);
        Save(items);
    }

    public void Clear()
    {
        Session.Remove(SessionKey);
        RemovePromo();
    }

    public int Count() => GetItems().Sum(i => i.Quantity);

    public decimal SubTotal() => GetItems().Sum(i => i.LineTotal);

    // Free delivery once the order is large enough.
    public decimal DeliveryCost() =>
        GetItems().Count == 0 || SubTotal() >= FreeDeliveryThreshold ? 0m : DeliveryFee;

    // ----- Promo code (stored in session once validated by the controller) -----

    private const string PromoCodeKey = "PromoCode";
    private const string PromoPercentKey = "PromoPercent";

    public void SetPromo(string code, int percent)
    {
        Session.SetString(PromoCodeKey, code);
        Session.SetInt32(PromoPercentKey, percent);
    }

    public void RemovePromo()
    {
        Session.Remove(PromoCodeKey);
        Session.Remove(PromoPercentKey);
    }

    public string? PromoCode() => Session.GetString(PromoCodeKey);
    public int PromoPercent() => Session.GetInt32(PromoPercentKey) ?? 0;

    public decimal Discount() => Math.Round(SubTotal() * PromoPercent() / 100m, 2);

    public decimal Total() => SubTotal() - Discount() + DeliveryCost();
}
