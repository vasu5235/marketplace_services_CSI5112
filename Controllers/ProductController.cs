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
    public async Task<ActionResult<List<Product>>> Get()
    {
        List<Product> products = await _productService.GetProducts();
        if (products.Count == 0)
            return NoContent();
        else
            return products;
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

    [HttpGet("search/{productName}")]
    public async Task<List<Product>> SearchProducts(String productName)
    {
        Console.WriteLine("--- debug ---- product.name: " + productName);

        List<Product> searchedProducts = await _productService.SearchProducts(productName);

        if (searchedProducts.Count == 0)
            return new List<Product>();
        return searchedProducts;
    }

    [HttpGet("search-cat/{categoryName}")]
    public async Task<List<Product>> SearchCategoryProducts(String categoryName)
    {
        Console.WriteLine("--- debug ---- product.name: " + categoryName);

        List<Product> searchedProducts = await _productService.SearchCategoryProducts(categoryName);

        if (searchedProducts.Count == 0)
            return new List<Product>();
        return searchedProducts;
    }

    [HttpPost]
    public async Task<ActionResult<bool>> AddProduct(Product product)
    {
        bool result = await _productService.AddProduct(product);

        return result;
    }

    [HttpPut]
    public async Task<ActionResult<bool>> EditProduct (Product product)
    {
        bool result = await _productService.EditProduct(product);
        if (result == false)
            return NotFound();
        return result;
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult<bool>> DeleteProduct(int Id)
    {
        bool result = await _productService.DeleteProductById(Id);
        return result;
    }
}

