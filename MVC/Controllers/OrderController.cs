using AutoMapper;
using Domain.Dtos.Orders;
using Domain.Filters;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MVC.Controllers;

public class OrderController(DataContext context, IOrderServices service, IMapper mapper) : Controller
{
    public SelectList Customers { get; set; }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] OrderFilter filter)
    {
        var orders = await service.GetAllOrdersAsync(filter);
        if (!orders.IsSuccess)
        {
            return View("Error");
        }

        return View(orders.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Customers = new SelectList(
            await context.Customers.ToListAsync(),
            "Id",
            "FullName"
        );
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto order)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Customers = new SelectList(
                await context.Customers.ToListAsync(),
                "Id",
                "FullName"
            );
            return View(order);
        }

        order.OrderDate = order.OrderDate.ToUniversalTime();
        await service.CreateOrderAsync(order);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        ViewBag.Customers = new SelectList(
            await context.Customers.ToListAsync(),
            "Id",
            "FullName"
        );
        var orderDto = await service.GetOrderByIdAsync(id);
        if (!orderDto.IsSuccess)
        {
            return NotFound();
        }

        var dto = mapper.Map<UpdateOrderDto>(orderDto.Data);
        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, UpdateOrderDto orderDto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Customers = new SelectList(
                await context.Customers.ToListAsync(),
                "Id",
                "FullName"
            );
            return View(orderDto);
        }

        orderDto.OrderDate = orderDto.OrderDate.ToUniversalTime();
        await service.UpdateOrderAsync(id, orderDto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var exist = await service.GetOrderByIdAsync(id);
        if (!exist.IsSuccess)
        {
            return NotFound();
        }

        return View(exist.Data);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var exist = await service.GetOrderByIdAsync(id);
        if (!exist.IsSuccess)
        {
            return NotFound();
        }

        await service.DeleteOrderAsync(id);
        return RedirectToAction("Index");
    }
}