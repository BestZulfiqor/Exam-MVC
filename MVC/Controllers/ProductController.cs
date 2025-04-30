using AutoMapper;
using Domain.Dtos.Products;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class ProductController(IProductService service, IMapper mapper) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery]ProductFilter filter)
    {
        var products = await service.GetAllProductsAsync(filter);
        if (!products.IsSuccess)
        {
            return View("Error");
        }
        return View(products.Data);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto productDto)
    {
        if (!ModelState.IsValid)
        {
            return View(productDto);
        }

        await service.CreateProductAsync(productDto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await service.GetProductByIdAsync(id);
        if (!product.IsSuccess)
        {
            return NotFound();
        }
        var dto = mapper.Map<UpdateProductDto>(product.Data);
        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, UpdateProductDto productDto)
    {
        if (!ModelState.IsValid)
        {
            return View(productDto);
        }

        await service.UpdateProductAsync(id, productDto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var exist = await service.GetProductByIdAsync(id);
        if (!exist.IsSuccess)
        {
            return NotFound();
        }
        return View(exist.Data);
    }
    
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var exist = await service.GetProductByIdAsync(id);
        if (!exist.IsSuccess)
        {
            return NotFound();
        }
        await service.DeleteProductAsync(id);
        return RedirectToAction("Index");
    }
}