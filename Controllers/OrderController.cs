using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_services_CSI5112.Models;
using marketplace_services_CSI5112.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace marketplace_services_CSI5112.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrderController(OrderService os)
    {
        this._orderService = os;
    }

    [HttpGet]
    public async Task<ActionResult<Dictionary<String, List<Product>>>> Get()
    {
        return await _orderService.GetOrders();
    }

    [HttpGet("byUser/{UserId}")]
    public async Task<ActionResult<Dictionary<String, List<Product>>>> GetOrdersByUser(String UserId)
    {
        Dictionary<String, List<Product>> FilteredOrders = await _orderService.GetOrdersByUserId(UserId);

        //maybe used here
        //if (FilteredOrders.Count == 0)
        //    return NotFound();
        return FilteredOrders;
    }

    [HttpGet("byOrder/{OrderId}")]
    public async Task<ActionResult<List<Product>>> GetOrder(String OrderId)
    {
        Console.WriteLine("--- debug ---- order.Id: " + OrderId);

        //maybe used here
        //bool orderExists = await _orderService.OrderExists(id);

        //if (!orderExists)
        //    return NotFound();

        List<Product> order = await _orderService.GetOrdersByOrderId(OrderId);
        return order;
    }

    [HttpPost]
    public async Task<ActionResult<bool>> AddOrder(String id, List<Product> products)
    {
        bool result = await _orderService.AddOrder(id, products);

        return result;
    }
}

