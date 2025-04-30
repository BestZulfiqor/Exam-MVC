using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Customers;

public class 
    CreateCustomerDto
{
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    [Required,  EmailAddress]
    public string Email { get; set; }
}
