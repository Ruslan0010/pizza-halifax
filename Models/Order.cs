namespace web.Models;

// A placed order. Customer details are captured at checkout; UserId links to
// an account when the customer was logged in (null for guest checkout).
public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public string DeliveryAddress { get; set; } = string.Empty;
    public string? Notes { get; set; }

    public string? UserId { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public decimal SubTotal { get; set; }
    public decimal DeliveryFee { get; set; }
    public decimal Total { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<OrderItem> Items { get; set; } = new();
}
