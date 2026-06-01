using web.Models;

namespace web.ViewModels;

// The storefront menu, optionally filtered by a search term and/or category.
public class MenuViewModel
{
    public List<Category> Categories { get; set; } = new();   // categories with matching products
    public List<Category> AllCategories { get; set; } = new(); // for the filter pills
    public string? Query { get; set; }
    public int? SelectedCategoryId { get; set; }
    public int ResultCount { get; set; }

    public bool IsFiltering => !string.IsNullOrWhiteSpace(Query) || SelectedCategoryId is not null;
}
