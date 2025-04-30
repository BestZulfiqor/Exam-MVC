using Domain.Dtos.OrderItems;

namespace Domain.Dtos.Orders;

public class CreateOrderDto
{
    public int CustomerId { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public List<GetOrderItemDto> Items { get; set; }
}
