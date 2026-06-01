namespace web.Models;

// An optional extra ingredient a customer can add to a pizza.
public class Topping
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int DisplayOrder { get; set; }
}
