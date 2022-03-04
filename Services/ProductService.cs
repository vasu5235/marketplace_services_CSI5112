using System;
using marketplace_services_CSI5112.Models;

namespace marketplace_services_CSI5112.Services
{
    public class CategoryService
    {
        private readonly List<Category> categories = new()
        {
            new Category(1, "Cloth", "images/category_images/01.png"),
            new Category(2, "Electronics", "images/category_images/02.png"),
            new Category(3, "Food", "images/category_images/03.png"),
            new Category(4, "Sports", "images/category_images/04.png"),
            new Category(5, "Books", "images/category_images/05.png"),
            new Category(6, "Health Care", "images/category_images/06.png"),
            new Category(7, "Category-1", "images/category_images/07.png"),
            new Category(8, "Category-2", "images/category_images/08.png"),

        };

        public CategoryService()
        {
        }

        public List<Category> GetCategories()
        {
            return this.categories;
        }

        public async Task<Category> GetCategory(int Id)
        {
            return categories.Find(x => x.Id.Equals(Id));
        }


        // returns false if category exists with newCategory.Id
        public async Task<bool> CreateCategory(Category newCategory)
        {
            Category existingCategory = categories.Find(x => x.Id == newCategory.Id);
            
            if (existingCategory == null)
            {
                categories.Add(newCategory);
                return true;
            }

            return false;

        }
    }
}

