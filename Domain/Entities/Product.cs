using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Product
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50)]
    public string Name { get; set; }
    [Required(ErrorMessage = "Price is required")]
    public decimal Price { get; set; }

    public List<OrderItem> OrderItems { get; set; }
}
