using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace web.ViewModels;

// Backs the admin create/edit product form. A dedicated view model (rather than
// binding the entity directly) keeps the form fields explicit.
public class ProductFormViewModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Image path")]
    public string ImageUrl { get; set; } = string.Empty;

    [Range(0.01, 1000)]
    [Display(Name = "Base price")]
    public decimal BasePrice { get; set; }

    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    [Display(Name = "Available")]
    public bool IsAvailable { get; set; } = true;

    [Display(Name = "Customizable (pizza sizes & toppings)")]
    public bool IsCustomizable { get; set; }

    public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
}
