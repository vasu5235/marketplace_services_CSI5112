using System;
using marketplace_services_CSI5112.Models;

namespace marketplace_services_CSI5112.Services
{
    public class CategoryService
    {
        private List<Category> categories = new()
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

        public async Task<List<Category>> GetCategories()
        {
            return this.categories;
        }

        public async Task<Category> GetCategory(int Id)
        {
            return categories.Find(x => x.Id.Equals(Id));
        }


        // returns false if category exists with newCategory.Id or newCategory.Name
        public async Task<bool> CreateCategory(Category newCategory)
        {
            // search for existing category with same id or same name.
            Category existingCategory = categories.Find(x => (x.Id == newCategory.Id) || (x.Name.Equals(newCategory.Name)));
            
            if (existingCategory == null)
            {
                categories.Add(newCategory);
                return true;
            }

            return false;

        }

        public async Task<string> EditCategory(Category editedCategory)
        {
            Category categoryWithSameName = categories.Find(x => x.Name.ToLower().Equals(editedCategory.Name.ToLower()));

            if (categoryWithSameName != null)
                return null;
            else
            {
                int existingCategoryIndex = categories.FindIndex(x => (x.Id == editedCategory.Id));
                if (existingCategoryIndex == -1)
                    return null;
                else
                {
                    string existingCategoryName = this.categories[existingCategoryIndex].Name;
                    this.categories[existingCategoryIndex] = editedCategory;
                    return existingCategoryName;
                }
            }

        }

        // returns false if category does not exists with categoryId
        public async Task<Category> DeleteCategory(int categoryId)
        {
            Category existingCategory = categories.Find(x => x.Id == categoryId);

            if (existingCategory != null)
            {
                System.Diagnostics.Debug.WriteLine("Deleting category: " + existingCategory.Id);
                this.categories = categories.Where(x => !x.Id.Equals(categoryId)).ToList();
            }
            
            return existingCategory;

        }
    }
}

