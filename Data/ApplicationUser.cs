using Microsoft.AspNetCore.Identity;

namespace web.Data;

// Application user. Extends IdentityUser so we can add profile fields later
// (e.g. full name, default delivery address) without another table.
public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}
