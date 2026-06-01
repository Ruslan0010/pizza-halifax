using web.Models;

namespace web.ViewModels;

// Data for the product detail page. Sizes and Toppings are empty for
// non-customizable items (drinks, sides).
public class ProductDetailsViewModel
{
    public Product Product { get; set; } = null!;
    public List<PizzaSize> Sizes { get; set; } = new();
    public List<Topping> Toppings { get; set; } = new();
}
