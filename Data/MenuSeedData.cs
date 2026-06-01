using Microsoft.EntityFrameworkCore;
using web.Models;

namespace web.Data;

// Static menu seeded via migrations, so a fresh database is ready to browse.
// Image files live in wwwroot/images/menu (added in the UI phase).
public static class MenuSeedData
{
    public static void Apply(ModelBuilder builder)
    {
        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Pizzas", DisplayOrder = 1 },
            new Category { Id = 2, Name = "Sides", DisplayOrder = 2 },
            new Category { Id = 3, Name = "Drinks", DisplayOrder = 3 },
            new Category { Id = 4, Name = "Desserts", DisplayOrder = 4 });

        builder.Entity<PizzaSize>().HasData(
            new PizzaSize { Id = 1, Name = "Small (10\")", PriceModifier = 0.00m, DisplayOrder = 1 },
            new PizzaSize { Id = 2, Name = "Medium (12\")", PriceModifier = 3.00m, DisplayOrder = 2 },
            new PizzaSize { Id = 3, Name = "Large (14\")", PriceModifier = 6.00m, DisplayOrder = 3 });

        builder.Entity<Topping>().HasData(
            new Topping { Id = 1, Name = "Extra cheese", Price = 1.50m, DisplayOrder = 1 },
            new Topping { Id = 2, Name = "Pepperoni", Price = 2.00m, DisplayOrder = 2 },
            new Topping { Id = 3, Name = "Mushrooms", Price = 1.00m, DisplayOrder = 3 },
            new Topping { Id = 4, Name = "Bacon", Price = 2.00m, DisplayOrder = 4 },
            new Topping { Id = 5, Name = "Onions", Price = 0.75m, DisplayOrder = 5 },
            new Topping { Id = 6, Name = "Bell peppers", Price = 1.00m, DisplayOrder = 6 },
            new Topping { Id = 7, Name = "Olives", Price = 1.00m, DisplayOrder = 7 },
            new Topping { Id = 8, Name = "Jalapeños", Price = 0.75m, DisplayOrder = 8 });

        builder.Entity<Product>().HasData(
            // Pizzas (customizable)
            P(1, "Margherita", "Classic tomato sauce, fresh mozzarella and basil.", "margherita", 11.99m, 1, true),
            P(2, "Pepperoni", "Tomato sauce, mozzarella and a generous layer of pepperoni.", "pepperoni", 13.99m, 1, true),
            P(3, "Hawaiian", "Ham, pineapple and mozzarella on a tomato base.", "hawaiian", 13.49m, 1, true),
            P(4, "Meat Lovers", "Pepperoni, bacon, ham, ground beef and mozzarella.", "meat-lovers", 15.99m, 1, true),
            P(5, "Veggie Supreme", "Mushrooms, peppers, onions, olives and tomatoes.", "veggie-supreme", 13.99m, 1, true),
            P(6, "BBQ Chicken", "Grilled chicken, red onion and BBQ sauce.", "bbq-chicken", 14.99m, 1, true),
            P(7, "Four Cheese", "Mozzarella, cheddar, parmesan and blue cheese.", "four-cheese", 13.99m, 1, true),
            P(8, "Spicy Diavola", "Spicy salami, chilli flakes and jalapeños.", "diavola", 14.49m, 1, true),
            // Sides
            P(9, "Garlic Bread", "Oven-baked bread with garlic butter and herbs.", "garlic-bread", 5.99m, 2, false),
            P(10, "Chicken Wings (8 pc)", "Crispy wings with your choice of dip.", "chicken-wings", 9.99m, 2, false),
            P(11, "Caesar Salad", "Romaine, parmesan, croutons and Caesar dressing.", "caesar-salad", 7.49m, 2, false),
            // Drinks
            P(12, "Coca-Cola (500 ml)", "Chilled bottle of Coca-Cola.", "coca-cola", 2.49m, 3, false),
            P(13, "Sparkling Water", "Refreshing sparkling mineral water.", "sparkling-water", 1.99m, 3, false),
            P(14, "Orange Juice", "Freshly squeezed orange juice.", "orange-juice", 3.49m, 3, false),
            // More pizzas
            P(15, "Capricciosa", "Ham, mushrooms, artichokes, olives and mozzarella.", "capricciosa", 14.49m, 1, true),
            P(16, "Marinara", "Tomato, garlic, oregano and olive oil — no cheese.", "marinara", 10.99m, 1, true),
            P(17, "Calzone", "Folded pizza stuffed with ham, mushrooms and mozzarella.", "calzone", 13.99m, 1, true),
            // More sides
            P(18, "Mozzarella Sticks", "Breaded mozzarella with a marinara dip.", "mozzarella-sticks", 6.99m, 2, false),
            P(19, "Onion Rings", "Crispy battered onion rings.", "onion-rings", 5.49m, 2, false),
            // More drinks
            P(20, "Sprite (500 ml)", "Chilled lemon-lime soda.", "sprite", 2.49m, 3, false),
            P(21, "Iced Tea", "Freshly brewed iced tea with lemon.", "iced-tea", 2.99m, 3, false),
            // Desserts
            P(22, "Tiramisu", "Classic Italian coffee and mascarpone dessert.", "tiramisu", 6.49m, 4, false),
            P(23, "Chocolate Brownie", "Warm fudgy chocolate brownie.", "chocolate-brownie", 5.49m, 4, false),
            P(24, "Cheesecake", "New York style cheesecake with berries.", "cheesecake", 6.99m, 4, false));
    }

    private static Product P(int id, string name, string description, string slug,
        decimal basePrice, int categoryId, bool customizable) => new()
    {
        Id = id,
        Name = name,
        Description = description,
        ImageUrl = $"/images/menu/{slug}.jpg",
        BasePrice = basePrice,
        CategoryId = categoryId,
        IsAvailable = true,
        IsCustomizable = customizable
    };
}
