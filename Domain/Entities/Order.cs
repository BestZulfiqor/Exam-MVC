namespace Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTimeOffset OrderDate { get; set; }

    public List<OrderItem> Items { get; set; }
    public Customer Customer { get; set; }
}
