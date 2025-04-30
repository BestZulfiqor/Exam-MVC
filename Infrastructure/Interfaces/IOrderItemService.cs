using Domain.Dtos.OrderItems;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IOrderItemService
{
    Task<Response<List<GetOrderItemDto>>> GetAllOrderItemsAsync(OrderItemFilter filter);
    Task<Response<GetOrderItemDto>> CreateOrderItemAsync(CreateOrderItemDto orderItemDto);
    Task<Response<GetOrderItemDto>> UpdateOrderItemAsync(int id, UpdateOrderItemDto orderItemDto);
    Task<Response<string>> DeleteOrderItemAsync(int id);
    Task<Response<GetOrderItemDto>> GetOrderItemByIdAsync(int id);
}
