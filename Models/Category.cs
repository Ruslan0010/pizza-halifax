namespace web.Models;

// A menu section, e.g. "Pizzas", "Sides", "Drinks".
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }

    public List<Product> Products { get; set; } = new();
}
