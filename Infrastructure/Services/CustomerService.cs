using System.Net;
using AutoMapper;
using Domain.Dtos.Customers;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class CustomerService(DataContext context, IMapper mapper) : ICustomerService
{
    public async Task<Response<GetCustomerDto>> CreateCustomerAsync(CreateCustomerDto customerDto)
    {
        var customer = mapper.Map<Customer>(customerDto);
        await context.Customers.AddAsync(customer);
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetCustomerDto>(customer);
        return result == 0
            ? new Response<GetCustomerDto>(HttpStatusCode.BadRequest, "Customer not added")
            : new Response<GetCustomerDto>(dto);
    }

    public async Task<Response<string>> DeleteCustomerAsync(int id)
    {
        var exist = await context.Customers.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Customer not found");
        }
        context.Customers.Remove(exist);

        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Customer not deleted")
            : new Response<string>("Customer deleted");
    }

    public async Task<Response<List<GetCustomerDto>>> GetAllCustomersAsync(CustomerFilter filter)
    {
        var customers = context.Customers.AsQueryable();
        var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);

        if (filter.FullName != null)
        {
            customers = customers.Where(n => n.FullName.Contains(filter.FullName));
        }

        var data = mapper.Map<List<GetCustomerDto>>(customers);
        return new Response<List<GetCustomerDto>>(data);
    }

    public async Task<Response<GetCustomerDto>> UpdateCustomerAsync(int id, UpdateCustomerDto customerDto)
    {
        var exist = await context.Customers.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetCustomerDto>(HttpStatusCode.NotFound, "Customer not found");
        }
        exist.Email = customerDto.Email;
        exist.FullName = customerDto.FullName;
        exist.PhoneNumber = customerDto.PhoneNumber;

        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetCustomerDto>(exist);
        return result == 0
            ? new Response<GetCustomerDto>(HttpStatusCode.BadRequest, "Customer not updated")
            : new Response<GetCustomerDto>(dto);
    }
    
    public async Task<Response<GetCustomerDto>> GetCustomerById(int id)
    {
        var exist = await context.Customers.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetCustomerDto>(HttpStatusCode.NotFound, "Customer not found");
        }

        var dto = mapper.Map<GetCustomerDto>(exist);
        return new Response<GetCustomerDto>(dto);
    }
}
