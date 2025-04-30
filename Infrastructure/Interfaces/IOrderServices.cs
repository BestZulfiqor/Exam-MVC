using Domain.Dtos.Orders;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IOrderServices
{
    Task<Response<List<GetOrderDto>>> GetAllOrdersAsync(OrderFilter filter);
    Task<Response<GetOrderDto>> CreateOrderAsync(CreateOrderDto orderDto);
    Task<Response<GetOrderDto>> UpdateOrderAsync(int id, UpdateOrderDto orderDto);
    Task<Response<string>> DeleteOrderAsync(int id);
    Task<Response<GetOrderDto>> GetOrderByIdAsync(int id);
}
