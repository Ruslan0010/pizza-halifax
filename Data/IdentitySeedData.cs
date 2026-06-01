using Microsoft.AspNetCore.Identity;

namespace web.Data;

// Ensures the Admin/Customer roles and a default admin account exist.
// Runs at startup so a freshly migrated database is immediately usable.
public static class IdentitySeedData
{
    public const string AdminEmail = "admin@pizzahalifax.local";
    public const string AdminPassword = "Admin123!";

    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        foreach (var role in new[] { "Admin", "Customer" })
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        if (await userManager.FindByEmailAsync(AdminEmail) is null)
        {
            var admin = new ApplicationUser
            {
                UserName = AdminEmail,
                Email = AdminEmail,
                EmailConfirmed = true,
                FullName = "Store Admin"
            };
            await userManager.CreateAsync(admin, AdminPassword);
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}
