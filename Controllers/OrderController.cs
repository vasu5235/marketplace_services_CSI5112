﻿using System;
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
        Dictionary<String, List<Product>> allOrders = await _orderService.GetOrders();

        if (allOrders.Keys.Count == 0)
            return NotFound("No orders in database");
        return await _orderService.GetOrders();
    }

    [HttpGet("byUser/{UserId}")]
    public async Task<ActionResult<Dictionary<String, List<Product>>>> GetOrdersByUser(String UserId)
    {
        if (UserId == null)
            return NotFound("Userid cannot be null");

        Dictionary<String, List<Product>> FilteredOrders = await _orderService.GetOrdersByUserId(UserId);

        if (FilteredOrders.Keys.Count == 0)
            return NotFound("No Products found for this user");

        return FilteredOrders;
    }

    [HttpGet("byOrder/{OrderId}")]
    public async Task<ActionResult<List<Product>>> GetOrder(String OrderId)
    {
        if (OrderId == null)
            return NotFound("Orderid cannot be null");
        Console.WriteLine("--- debug ---- order.Id: " + OrderId);

        List<Product> order = await _orderService.GetOrdersByOrderId(OrderId);

        if (order.Count == 0)
            return NotFound("No products found for this orderid");

        return order;
    }

    [HttpPost]
    public async Task<ActionResult<bool>> AddOrder(String id, List<Product> products)
    {
        if (id == null || products.Count == 0)
            return NotFound("Either id is null or productList is empty");

        if (id.Split('-').Length != 2)
            return NotFound("OrderId path param should be in format 'userId-orderId' example 123-456");

        bool result = await _orderService.AddOrder(id, products);

        return result;
    }
}

