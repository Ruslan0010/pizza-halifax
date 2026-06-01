namespace web.Models;

// A pizza size. PriceModifier is added to a product's BasePrice,
// e.g. Small +0, Medium +3.00, Large +6.00.
public class PizzaSize
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal PriceModifier { get; set; }
    public int DisplayOrder { get; set; }
}
