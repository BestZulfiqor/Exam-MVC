using AutoMapper;
using Domain.Entities;
using Domain.Dtos.Customers;
using Domain.Dtos.Products;
using Domain.Dtos.Orders;
using Domain.Dtos.OrderItems;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Customer, GetCustomerDto>();
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<UpdateCustomerDto, Customer>();
        CreateMap<GetCustomerDto, UpdateCustomerDto>();

        CreateMap<Product, GetProductDto>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        CreateMap<GetProductDto, UpdateProductDto>();

        CreateMap<Order, GetOrderDto>();
        CreateMap<CreateOrderDto, Order>();
        CreateMap<UpdateOrderDto, Order>();
        CreateMap<GetOrderDto, UpdateOrderDto>();

        CreateMap<OrderItem, GetOrderItemDto>();
        CreateMap<CreateOrderItemDto, OrderItem>();
        CreateMap<UpdateOrderItemDto, OrderItem>();
        CreateMap<GetOrderItemDto, UpdateOrderItemDto>();
    }
}