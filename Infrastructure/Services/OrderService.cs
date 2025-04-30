using System.Net;
using AutoMapper;
using Domain.Dtos.Orders;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class OrderService(DataContext context, IMapper mapper) : IOrderServices
{
    public async Task<Response<GetOrderDto>> CreateOrderAsync(CreateOrderDto orderDto)
    {
        var order = mapper.Map<Order>(orderDto);
        await context.Orders.AddAsync(order);
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetOrderDto>(order);
        return result == 0
            ? new Response<GetOrderDto>(HttpStatusCode.BadRequest, "Order not added")
            : new Response<GetOrderDto>(dto);
    }

    public async Task<Response<string>> DeleteOrderAsync(int id)
    {
        var exist = await context.Orders.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Order not found");
        }
        context.Orders.Remove(exist);

        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Order not deleted")
            : new Response<string>("Order deleted");
    }

    public async Task<Response<List<GetOrderDto>>> GetAllOrdersAsync(OrderFilter filter)
    {
        var orders = context.Orders.AsQueryable();
        var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);

        if (filter.FromOrderDate.HasValue)
        {
            orders = orders.Where(n => n.OrderDate.Date >= filter.FromOrderDate.Value.Date);
        }

        if (filter.ToOrderDate.HasValue)
        {
            orders = orders.Where(n => n.OrderDate.Date <= filter.ToOrderDate.Value.Date);
        }

        var data = mapper.Map<List<GetOrderDto>>(orders);
        return new Response<List<GetOrderDto>>(data);
    }

    public async Task<Response<GetOrderDto>> UpdateOrderAsync(int id, UpdateOrderDto orderDto)
    {
        var exist = await context.Orders.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetOrderDto>(HttpStatusCode.NotFound, "Order not found");
        }
        exist.CustomerId = orderDto.CustomerId;
        exist.OrderDate = orderDto.OrderDate;

        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetOrderDto>(exist);
        return result == 0
            ? new Response<GetOrderDto>(HttpStatusCode.BadRequest, "Order not updated")
            : new Response<GetOrderDto>(dto);
    }

    public async Task<Response<GetOrderDto>> GetOrderByIdAsync(int id)
    {
        var exist = await context.Orders.FindAsync(id);
        if (exist == null)
        {
            return new  Response<GetOrderDto>(HttpStatusCode.NotFound, "Order not found");
        }
        var dto = mapper.Map<GetOrderDto>(exist);
        return new Response<GetOrderDto>(dto);
    }
}
