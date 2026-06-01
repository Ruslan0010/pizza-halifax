namespace web.Services;

// One line in the shopping cart. Lines with the same product, size and
// toppings share a Key so they stack instead of duplicating.
public class CartItem
{
    public string Key { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public int? SizeId { get; set; }
    public string? SizeName { get; set; }
    public string ToppingsSummary { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }

    public decimal LineTotal => UnitPrice * Quantity;
}
