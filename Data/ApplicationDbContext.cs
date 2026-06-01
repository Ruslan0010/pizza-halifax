using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using web.Models;

namespace web.Data;

// Single EF Core context for both Identity (users/roles) and the shop domain.
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<PizzaSize> PizzaSizes => Set<PizzaSize>();
    public DbSet<Topping> Toppings => Set<Topping>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        // All money columns use numeric(10,2).
        builder.Properties<decimal>().HavePrecision(10, 2);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        MenuSeedData.Apply(builder);
    }
}
