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
        List<Category> allCategories = await _categoryService.GetCategories();

        if (allCategories.Count == 0)
            return NotFound("No categories exist in database");

        return allCategories;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        if (id == null)
            return BadRequest("category id cannot be null");

        Console.WriteLine("--- debug ---- category.Id: " + id);

        Category category = await _categoryService.GetCategory(id);

        if (category == null)
            return NotFound("Category does not exist with this id");

        return category;
    }

    [HttpPost]
    public async Task<ActionResult<bool>> AddCategory(Category category)
    {
        if (category.Id == null || category.Name == null || category.ImageURL == null)
            return BadRequest("One of the body params was null");

        bool result = await _categoryService.CreateCategory(category);

        if (result == false)
            return BadRequest("Category with same id or name already exists");

        return true;
    }

    [HttpPut]
    public async Task<ActionResult<bool>> EditCategory(Category editedCategory)
    {
        if (editedCategory.Id == null || editedCategory.Name == null || editedCategory.ImageURL == null)
            return BadRequest("One of the body params was null");

        bool result = false;
        String oldCategoryName = await _categoryService.EditCategory(editedCategory);

        if (oldCategoryName == null)
            return BadRequest("No existing category found with the passed category id value");

        else if (!oldCategoryName.Equals(editedCategory.Name))
        {
            // if category name was changed, update all products to reflect this
            System.Diagnostics.Debug.WriteLine("Category edited, now editing all associated products");

            //always returns true; 
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
        if (Id == null)
            return BadRequest("Id cannot be null");

        bool result = false;

        Category deletedCategory = await _categoryService.DeleteCategory(Id);

        if (deletedCategory == null)
            return NotFound("No category found with passed Id param");
        else
        {
            //delete all associated products with this category
            System.Diagnostics.Debug.WriteLine("Category deleted, now deleting all associated products");
            //always returns true;
            result = await _productService.DeleteProductsByCategory(deletedCategory.Name);
        }

        return result;
    }
}

