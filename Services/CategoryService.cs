using System;
using marketplace_services_CSI5112.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace marketplace_services_CSI5112.Services
{
    public class CategoryService
    {
        //private List<Category> categories = new()
        //{
        //    new Category("", 1, "Cloth", "images/category_images/01.png"),
        //    new Category("", 2, "Electronics", "images/category_images/02.png"),
        //    new Category("", 3, "Food", "images/category_images/03.png"),
        //    new Category("", 4, "Sports", "images/category_images/04.png"),
        //    new Category("", 5, "Books", "images/category_images/05.png"),
        //    new Category("", 6, "Health Care", "images/category_images/06.png"),
        //    new Category("", 7, "Category-1", "images/category_images/07.png"),
        //    new Category("", 8, "Category-2", "images/category_images/08.png"),

        //};
        private readonly IMongoCollection<Category> _categories;
        private readonly string CONNECTION_STRING;

        public CategoryService(IOptions<DatabaseSettings> DatabaseSettings, IConfiguration configuration)
        {
            string CONNECTION_STRING = configuration.GetValue<string>("CONNECTION_STRING");

            if (string.IsNullOrEmpty(CONNECTION_STRING))
            {
                // default - should not be used
                System.Console.WriteLine("*********WARNING: USING DEFAULT CONNECTION STRING!!!!!*********");
                System.Diagnostics.Debug.WriteLine("*********WARNING: USING DEFAULT CONNECTION STRING!!!!!*********");
                CONNECTION_STRING = "mongodb+srv://admin_user:QKVmntvNB1QgVBe3@cluster0.pd3bf.mongodb.net/Marketplace?retryWrites=true&w=majority";
            }
            else
            {
                System.Console.WriteLine("*********SUCCESS: USING DB STRING FROM ENV!!!!!*********");
                System.Diagnostics.Debug.WriteLine("*********SUCCESS: USING DB STRING FROM ENV!!!!!*********");
            }
            this.CONNECTION_STRING = CONNECTION_STRING;

            var settings = MongoClientSettings.FromConnectionString(CONNECTION_STRING);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("Marketplace");
            _categories = database.GetCollection<Category>("Category");
        }

        public async Task<List<Category>> GetCategories()
        {
            //return this.categories;
            return await _categories.Find(_ => true).ToListAsync();
        }

        public async Task<Category> GetCategory(int Id)
        {
            //return categories.Find(x => x.Id.Equals(Id));
            return await _categories.Find(x => x.Id == Id).FirstOrDefaultAsync();
        }


        // returns false if category exists with newCategory.Id or newCategory.Name
        public async Task<bool> CreateCategory(Category newCategory)
        {
            // search for existing category with same id or same name.
            //Category existingCategory = categories.Find(x => x.Id == newCategory.Id || x.Name.Equals(newCategory.Name) );
            Category existingCategory = await _categories.Find(x => x.Id == newCategory.Id || x.Name.Equals(newCategory.Name)).FirstOrDefaultAsync();

            if (existingCategory == null)
            {
                //categories.Add(newCategory);
                await _categories.InsertOneAsync(newCategory);
                return true;
            }

            return false;

        }

        public async Task<string> EditCategory(Category editedCategory)
        {
            //int existingCategoryIndex = categories.FindIndex(x => (x.Id == editedCategory.Id));
            Category existingCategory = await _categories.Find(x => x.Id == editedCategory.Id).FirstOrDefaultAsync();

            //return if no category exists with this id
            if (existingCategory == null)
                return null;
            else
            {
                string existingCategoryName = existingCategory.Name;

                editedCategory.MongoId = existingCategory.MongoId;

                //this.categories[existingCategoryIndex] = editedCategory;
                await _categories.ReplaceOneAsync(x => x.Id == existingCategory.Id, editedCategory);
                return existingCategoryName;
            }
            //Category categoryWithSameName = categories.Find(x => x.Name.ToLower().Equals(editedCategory.Name.ToLower()));

            //if (categoryWithSameName != null)
            //    return "";

            //else
            //{
            //    int existingCategoryIndex = categories.FindIndex(x => (x.Id == editedCategory.Id));
            //    if (existingCategoryIndex == -1)
            //        return null;
            //    else
            //    {
            //        string existingCategoryName = this.categories[existingCategoryIndex].Name;
            //        this.categories[existingCategoryIndex] = editedCategory;
            //        return existingCategoryName;
            //    }
            //}

        }

        // returns false if category does not exists with categoryId
        public async Task<Category> DeleteCategory(int categoryId)
        {
            //find existingcategory and return it so that all associated products can be deleted.
            //Category existingCategory = categories.Find(x => x.Id == categoryId);
            Category existingCategory = await _categories.Find(x => x.Id == categoryId).FirstOrDefaultAsync();

            if (existingCategory != null)
            {
                //System.Diagnostics.Debug.WriteLine("Deleting category: " + existingCategory.Id);
                //this.categories = categories.Where(x => !x.Id.Equals(categoryId)).ToList();
                DeleteResult result = await _categories.DeleteOneAsync(x => x.Id == categoryId);
            }
            
            return existingCategory;

        }
    }
}

