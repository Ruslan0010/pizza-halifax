namespace web.Models;

// A single line in an order. Product details are snapshotted (copied) at
// checkout so the order stays accurate even if the menu changes later.
public class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order? Order { get; set; }

    public int? ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;
    public string? SizeName { get; set; }

    // Comma-separated extra toppings, e.g. "Extra cheese, Mushrooms". Empty if none.
    public string ToppingsSummary { get; set; } = string.Empty;

    // Price for one unit (base + size + toppings) at the time of ordering.
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal { get; set; }
}
