using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }

    public List<Order> Orders { get; set; }
}
