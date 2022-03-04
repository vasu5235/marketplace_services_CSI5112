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
    public Dictionary<int, List<Product>> Get()
    {
        return _orderService.GetOrders();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<Product>>> GetOrder(int id)
    {
        Console.WriteLine("--- debug ---- order.Id: " + id);

        bool orderExists = await _orderService.OrderExists(id);

        if (!orderExists)
            return NotFound();

        else
        {
            List<Product> order = await _orderService.GetOrder(id);
            return order;
        }
    }

    [HttpPost]
    public async Task<ActionResult<bool>> AddOrder(int id, List<Product> products)
    {
        bool result = await _orderService.AddOrder(id, products);

        return result;
    }
}

