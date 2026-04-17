using System.ComponentModel.DataAnnotations;

namespace RestaurantOrderAPI.DTOs;

public class ProductCreateDto
{
    [Required]
    public string Name { get; set; }
    [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser maior que zero")]
    public decimal Price { get; set; }
}
