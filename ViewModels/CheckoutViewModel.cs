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

    // "Delivery" or "Pickup". Address is only required for delivery
    // (validated in the controller).
    [Required]
    public string OrderType { get; set; } = "Delivery";

    [Display(Name = "Delivery address")]
    public string? DeliveryAddress { get; set; }

    [Display(Name = "Delivery notes (optional)")]
    public string? Notes { get; set; }

    [Required]
    [Display(Name = "Payment method")]
    public string PaymentMethod { get; set; } = "Cash on delivery";
}
