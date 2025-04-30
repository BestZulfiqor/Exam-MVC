using System.Net;
using AutoMapper;
using Domain.Dtos.OrderItems;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class OrderItemService(DataContext context, IMapper mapper) : IOrderItemService
{
    public async Task<Response<GetOrderItemDto>> CreateOrderItemAsync(CreateOrderItemDto orderItemDto)
    {
        var orderItem = mapper.Map<OrderItem>(orderItemDto);
        await context.OrderItems.AddAsync(orderItem);
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetOrderItemDto>(orderItem);
        return result == 0
            ? new Response<GetOrderItemDto>(HttpStatusCode.BadRequest, "OrderItem not added")
            : new Response<GetOrderItemDto>(dto);
    }

    public async Task<Response<string>> DeleteOrderItemAsync(int id)
    {
        var exist = await context.OrderItems.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "OrderItem not found");
        }
        context.OrderItems.Remove(exist);

        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "OrderItem not deleted")
            : new Response<string>("OrderItem deleted");
    }

    public async Task<Response<List<GetOrderItemDto>>> GetAllOrderItemsAsync(OrderItemFilter filter)
    {
        var OrderItems = context.OrderItems.AsQueryable();
        var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);

        if (filter.FromQuantity != null)
        {
            OrderItems = OrderItems.Where(n => n.Quantity >= filter.FromQuantity);
        }

        if (filter.ToQuantity != null)
        {
            OrderItems = OrderItems.Where(n => n.Quantity <= filter.ToQuantity);
        }

        var data = mapper.Map<List<GetOrderItemDto>>(OrderItems);
        return new Response<List<GetOrderItemDto>>(data);
    }

    public async Task<Response<GetOrderItemDto>> UpdateOrderItemAsync(int id, UpdateOrderItemDto orderItemDto)
    {
        var exist = await context.OrderItems.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetOrderItemDto>(HttpStatusCode.NotFound, "OrderItem not found");
        }
        exist.OrderId = orderItemDto.OrderId;
        exist.ProductId = orderItemDto.ProductId;
        exist.Quantity = orderItemDto.Quantity;

        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetOrderItemDto>(exist);
        return result == 0
            ? new Response<GetOrderItemDto>(HttpStatusCode.BadRequest, "OrderItem not updated")
            : new Response<GetOrderItemDto>(dto);
    }

    public async Task<Response<GetOrderItemDto>> GetOrderItemByIdAsync(int id)
    {
        var exist = await context.OrderItems.FindAsync(id);
        if (exist == null)
        {
            return new PagedResponse<GetOrderItemDto>(HttpStatusCode.NotFound, "Order item not found");
        }
        var dto = mapper.Map<GetOrderItemDto>(exist);
        return new Response<GetOrderItemDto>(dto);
    }
}
