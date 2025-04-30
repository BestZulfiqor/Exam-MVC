using System.Net;
using AutoMapper;
using Domain.Dtos.Products;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class ProductService(DataContext context, IMapper mapper) : IProductService
{
    public async Task<Response<GetProductDto>> CreateProductAsync(CreateProductDto productDto)
    {
        var product = mapper.Map<Product>(productDto);
        await context.Products.AddAsync(product);
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetProductDto>(product);
        return result == 0
            ? new Response<GetProductDto>(HttpStatusCode.BadRequest, "Product not added")
            : new Response<GetProductDto>(dto);
    }

    public async Task<Response<string>> DeleteProductAsync(int id)
    {
        var exist = await context.Products.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Product not found");
        }
        context.Products.Remove(exist);

        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Product not deleted")
            : new Response<string>("Product deleted");
    }

    public async Task<Response<List<GetProductDto>>> GetAllProductsAsync(ProductFilter filter)
    {
        var products = context.Products.AsQueryable();
        var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);

        if (filter.FromPrice != null)
        {
            products = products.Where(n => n.Price >= filter.FromPrice);
        }

        if (filter.ToPrice != null)
        {
            products = products.Where(n => n.Price <= filter.ToPrice);
        }
        
        if (filter.Name != null)
        {
            products = products.Where(n => n.Name.Contains(filter.Name));
        }

        var data = mapper.Map<List<GetProductDto>>(products);
        return new Response<List<GetProductDto>>(data);
    }

    public async Task<Response<GetProductDto>> UpdateProductAsync(int id, UpdateProductDto productDto)
    {
        var exist = await context.Products.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetProductDto>(HttpStatusCode.NotFound, "Product not found");
        }
        exist.Name = productDto.Name;
        exist.Price = productDto.Price;

        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetProductDto>(exist);
        return result == 0
            ? new Response<GetProductDto>(HttpStatusCode.BadRequest, "Product not updated")
            : new Response<GetProductDto>(dto);
    }

    public async Task<Response<GetProductDto>> GetProductByIdAsync(int id)
    {
        var exist = await context.Products.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetProductDto>(HttpStatusCode.NotFound, "Product not found");
        }
        var dto = mapper.Map<GetProductDto>(exist);
        return new Response<GetProductDto>(dto);
    }
}
