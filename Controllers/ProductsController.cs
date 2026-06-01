using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.ViewModels;

namespace web.Controllers;

public class ProductsController : Controller
{
    private readonly ApplicationDbContext _db;

    public ProductsController(ApplicationDbContext db)
    {
        _db = db;
    }

    // Product detail page. Customizable products (pizzas) also load the size
    // and topping options.
    public async Task<IActionResult> Details(int id)
    {
        var product = await _db.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id && p.IsAvailable);

        if (product is null)
            return NotFound();

        var vm = new ProductDetailsViewModel { Product = product };

        if (product.IsCustomizable)
        {
            vm.Sizes = await _db.PizzaSizes.OrderBy(s => s.DisplayOrder).ToListAsync();
            vm.Toppings = await _db.Toppings.OrderBy(t => t.DisplayOrder).ToListAsync();
        }

        return View(vm);
    }
}
