using Domain.Dtos.Products;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IProductService
{
    Task<Response<List<GetProductDto>>> GetAllProductsAsync(ProductFilter filter);
    Task<Response<GetProductDto>> CreateProductAsync(CreateProductDto productDto);
    Task<Response<GetProductDto>> UpdateProductAsync(int id, UpdateProductDto productDto);
    Task<Response<string>> DeleteProductAsync(int id);
    Task<Response<GetProductDto>> GetProductByIdAsync(int id);
}
