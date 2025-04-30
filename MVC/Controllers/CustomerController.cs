using AutoMapper;
using Domain.Dtos.Customers;
using Domain.Filters;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class CustomerController(ICustomerService service, IMapper mapper) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] CustomerFilter filter)
    {
        var customers = await service.GetAllCustomersAsync(filter);
        if (!customers.IsSuccess)
        {
            return View("Error");
        }

        return View(customers.Data);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerDto customerDto)
    {
        if (!ModelState.IsValid)
        {
            return View(customerDto);
        }

        await service.CreateCustomerAsync(customerDto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var customer = await service.GetCustomerById(id);
        if (!customer.IsSuccess)
        {
            return NotFound();
        }

        var dto = mapper.Map<UpdateCustomerDto>(customer.Data);
        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, UpdateCustomerDto customerDto)
    {
        if (!ModelState.IsValid)
        {
            return View(customerDto);
        }

        await service.UpdateCustomerAsync(id, customerDto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var exist = await service.GetCustomerById(id);
        if (!exist.IsSuccess)
        {
            return NotFound();
        }
        return View(exist.Data);
    }
    
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var exist = await service.GetCustomerById(id);
        if (!exist.IsSuccess)
        {
            return NotFound();
        }
        await service.DeleteCustomerAsync(id);
        return RedirectToAction("Index");
    }
}