namespace web.Models;

// A discount code a customer can apply to their order.
public class PromoCode
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public int DiscountPercent { get; set; }
    public bool IsActive { get; set; } = true;
}
