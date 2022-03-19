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
public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;
    private readonly ProductService _productService;

    public CategoryController(CategoryService cs, ProductService ps)
    {
        this._categoryService = cs;
        this._productService = ps;
    }

    [HttpGet]
    public async Task<ActionResult<List<Category>>> Get()
    {
        return await _categoryService.GetCategories();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        Console.WriteLine("--- debug ---- category.Id: " + id);

        Category category = await _categoryService.GetCategory(id);
        if (category == null)
            return NotFound();
        return category;
    }

    [HttpPost]
    public async Task<ActionResult<bool>> AddCategory(Category category)
    {
        bool result = await _categoryService.CreateCategory(category);

        return result;
    }

    [HttpPut]
    public async Task<ActionResult<bool>> EditCategory(Category editedCategory)
    {
        bool result = false;
        String oldCategoryName = await _categoryService.EditCategory(editedCategory);
        if (oldCategoryName == null)
            return BadRequest();
        else if (!oldCategoryName.Equals(editedCategory.Name))
        {
            System.Diagnostics.Debug.WriteLine("Category edited, now editing all associated products");
            result = await _productService.EditProductsForCategory(oldCategoryName, editedCategory.Name);
        }
        else
        {
            result = true;
        }
        return result;
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult<bool>> DeleteCategory(int Id)
    {
        bool result = false;
        Category deletedCategory = await _categoryService.DeleteCategory(Id);

        if (deletedCategory == null)
            return NotFound();
        else
        {
            System.Diagnostics.Debug.WriteLine("Category deleted, now deleting all associated products");
            result = await _productService.DeleteProductsByCategory(deletedCategory.Name);
        }

        return result;
    }
}

