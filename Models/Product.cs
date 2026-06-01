namespace web.Models;

// A single item on the menu. BasePrice is the price of the smallest size
// (or the flat price for non-customizable items like drinks).
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal BasePrice { get; set; }

    public bool IsAvailable { get; set; } = true;

    // When true (pizzas), the detail page shows size and topping pickers.
    public bool IsCustomizable { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}
