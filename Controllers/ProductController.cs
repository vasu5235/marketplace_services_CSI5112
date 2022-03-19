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
            return NotFound("No Products exist in database");

        return products;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        if (id == null)
            return BadRequest("productId cannot be null");
        Console.WriteLine("--- debug ---- product.Id: " + id);

        Product product = await _productService.GetProduct(id);
        if (product == null)
            return NotFound("No product found for this id");
        return product;
    }

    [HttpGet("search/{productName}")]
    public async Task<ActionResult<List<Product>>> SearchProducts(String productName)
    {
        if (productName == null)
            return BadRequest("Product name cannot be null");
        Console.WriteLine("--- debug ---- product.name: " + productName);

        List<Product> searchedProducts = await _productService.SearchProducts(productName);

        if (searchedProducts.Count == 0)
            return NotFound("No products found for this product name");

        return searchedProducts;
    }

    [HttpGet("search-cat/{categoryName}")]
    public async Task<ActionResult<List<Product>>> SearchCategoryProducts(String categoryName)
    {
        if (categoryName == null)
            return BadRequest("Category name param cannot be null");

        Console.WriteLine("--- debug ---- product.name: " + categoryName);

        List<Product> searchedProducts = await _productService.SearchCategoryProducts(categoryName);

        if (searchedProducts.Count == 0)
            return NotFound("No products found for this category name");
       
        return searchedProducts;
    }

    [HttpPost]
    public async Task<ActionResult<bool>> AddProduct(Product product)
    {
        if (product.Id == null || product.Name == null || product.Price == null
            || product.Quantity == null || product.Description == null)
            return BadRequest("One of the body params is null");

        bool result = await _productService.AddProduct(product);

        if (result == false)
            return BadRequest("Product with same id or name exists");

        return result;
    }

    [HttpPut]
    public async Task<ActionResult<bool>> EditProduct (Product product)
    {
        if (product.Id == null || product.Name == null || product.Price == null
            || product.Quantity == null || product.Description == null)
            return BadRequest("One of the body params is null");

        bool result = await _productService.EditProduct(product);

        if (result == false)
            return NotFound("Product does not exist so cannot update");

        return result;
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult<bool>> DeleteProduct(int Id)
    {
        if (Id == null)
            return BadRequest("ProductId cannot be null");

        bool result = await _productService.DeleteProductById(Id);

        if (result == false)
            return BadRequest("Cannot delete product because it does not exist");

        return result;
    }
}

