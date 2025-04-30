using AutoMapper;
using Domain.Dtos.OrderItems;
using Domain.Dtos.Orders;
using Domain.Filters;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MVC.Controllers;

public class OrderItemController(IOrderItemService service, DataContext context, IMapper mapper) : Controller
{
    public SelectList Products { get; set; }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] OrderItemFilter filter)
    {
        var orders = await service.GetAllOrderItemsAsync(filter);
        if (!orders.IsSuccess)
        {
            return View("Error");
        }

        return View(orders.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Products = new SelectList(
            await context.Products.ToListAsync(),
            "Id",
            "Name"
        );
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderItemDto orderItem)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Products = new SelectList(
                await context.Products.ToListAsync(),
                "Id",
                "Name"
            );
            return View(orderItem);
        }

        await service.CreateOrderItemAsync(orderItem);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        ViewBag.Products = new SelectList(
            await context.Products.ToListAsync(),
            "Id",
            "Name"
        );
        var orderDto = await service.GetOrderItemByIdAsync(id);
        if (!orderDto.IsSuccess)
        {
            return NotFound();
        }

        var dto = mapper.Map<UpdateOrderItemDto>(orderDto.Data);
        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, UpdateOrderItemDto orderItemDto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Products = new SelectList(
                await context.Products.ToListAsync(),
                "Id",
                "Name"
            );
            return View(orderItemDto);
        }

        await service.UpdateOrderItemAsync(id, orderItemDto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var exist = await service.GetOrderItemByIdAsync(id);
        if (!exist.IsSuccess)
        {
            return NotFound();
        }

        return View(exist.Data);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var exist = await service.GetOrderItemByIdAsync(id);
        if (!exist.IsSuccess)
        {
            return NotFound();
        }

        await service.DeleteOrderItemAsync(id);
        return RedirectToAction("Index");
    }
}