using System.ComponentModel.DataAnnotations;

namespace web.ViewModels;

public class CheckoutViewModel
{
    [Required]
    [Display(Name = "Full name")]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [Phone]
    [Display(Name = "Phone number")]
    public string CustomerPhone { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Delivery address")]
    public string DeliveryAddress { get; set; } = string.Empty;

    [Display(Name = "Delivery notes (optional)")]
    public string? Notes { get; set; }

    [Required]
    [Display(Name = "Payment method")]
    public string PaymentMethod { get; set; } = "Cash on delivery";
}
