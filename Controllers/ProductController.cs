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
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService ps)
    {
        this._productService = ps;
    }

    [HttpGet]
    public List<Product> Get()
    {
        return _productService.GetProducts();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        Console.WriteLine("--- debug ---- product.Id: " + id);

        Product product = await _productService.GetProduct(id);
        if (product == null)
            return NotFound();
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<bool>> AddProduct(Product product)
    {
        bool result = await _productService.AddProduct(product);

        return result;
    }
}

