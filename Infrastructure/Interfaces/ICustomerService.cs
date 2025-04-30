using Domain.Dtos.Customers;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ICustomerService
{
    Task<Response<List<GetCustomerDto>>> GetAllCustomersAsync(CustomerFilter filter);
    Task<Response<GetCustomerDto>> CreateCustomerAsync(CreateCustomerDto customerDto);
    Task<Response<GetCustomerDto>> UpdateCustomerAsync(int id, UpdateCustomerDto customerDto);
    Task<Response<string>> DeleteCustomerAsync(int id);
    Task<Response<GetCustomerDto>> GetCustomerById(int id);
}
