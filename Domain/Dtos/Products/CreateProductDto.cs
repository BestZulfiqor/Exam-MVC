using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Products;

public class CreateProductDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Price is required")]
    public decimal Price { get; set; }
}
