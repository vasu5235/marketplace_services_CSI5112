using System;
using marketplace_services_CSI5112.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace marketplace_services_CSI5112.Services
{
    public class ProductService
    {
        //private List<Product> products = new()
        //{
        //    new Product("", 1, "Test-1","Sample description1", 100.0, "images/recent_images/01.jpeg","Electronics",1),
        //    new Product("", 2, "Test-2", "Sample description2", 200.0, "images/recent_images/02.jpeg", "Electronics",1),
        //    new Product("", 3, "Test-3", "Sample description3", 300.0, "images/recent_images/03.jpeg", "Electronics",1),
        //    new Product("", 4, "Test-4", "Sample description4", 400.0, "images/recent_images/04.jpeg", "Electronics",1),
        //    new Product("", 5, "Test-5", "Sample description5", 500.0, "images/recent_images/05.jpeg", "Food",1),
        //    new Product("", 6, "Test-6", "Sample description6", 600.0, "images/recent_images/06.jpeg", "Electronics",1),
        //};
        private readonly IMongoCollection<Product> _products;
        private readonly string CONNECTION_STRING;

        public ProductService(IOptions<DatabaseSettings> DatabaseSettings, IConfiguration configuration)
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
            _products = database.GetCollection<Product>("Product");
        }

        public async Task<List<Product>> GetProducts()
        {
            //return this.products;
            return await _products.Find(_ => true).ToListAsync();
        }

        public async Task<Product> GetProduct(int Id)
        {
            //return products.Find(x => x.Id == Id);
            return await _products.Find(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> SearchProducts(String productName)
        {
            //return products.FindAll(p => p.Name.ToLower().Contains(productName.ToLower()));
            return await _products.Find(x => x.Name.ToLower().Contains(productName.ToLower())).ToListAsync();
        }

        public async Task<List<Product>> SearchCategoryProducts(String categoryName)
        {
            //return products.FindAll(p => p.Category.ToLower().Contains(categoryName.ToLower()));
            return await _products.Find(x => x.Category.ToLower().Contains(categoryName.ToLower())).ToListAsync();
        }


        // returns false if product exists with newProduct.Id
        public async Task<bool> AddProduct(Product newProduct)
        {
            //Product existingProduct = products.Find(x => x.Id == newProduct.Id || x.Name.ToLower().Equals(newProduct.Name.ToLower()) );
            // mongoDB driver does not implement .ToLower(), for now we will handle it on the client side only. 
            Product existingProduct = await _products.Find(x => x.Id == newProduct.Id || x.Name.Equals(newProduct.Name)).FirstOrDefaultAsync();

            if (existingProduct == null)
            {
                //products.Add(newProduct);
                await _products.InsertOneAsync(newProduct);
                return true;
            }

            return false;

        }

        public async Task<bool> EditProduct(Product editedProduct)
        {
            //int existingProductIndex = products.FindIndex(x => x.Id == editedProduct.Id);
            Product existingProduct = await _products.Find(x => x.Id == editedProduct.Id).FirstOrDefaultAsync();

            if (existingProduct == null)
                return false;
            else
            {
                editedProduct.MongoId = existingProduct.MongoId;
                //this.products[existingProductIndex] = editedProduct;

                ReplaceOneResult r = await _products.ReplaceOneAsync(x => x.Id == editedProduct.Id, editedProduct);
                
                return true;
            }

        }

        public async Task<bool> EditProductsForCategory(string oldCategoryName, string newCategoryName)
        {
            //find all products associated with this category
            List<Product> productsInThisCategory = await SearchCategoryProducts(oldCategoryName);

            if (productsInThisCategory.Count != 0)
            {
                foreach (Product p in productsInThisCategory)
                {
                    System.Diagnostics.Debug.WriteLine("Editing categoryname for product: " + p.Name+ " with mongoid: "+p.MongoId);
                    //int index = this.products.FindIndex(x => x.Category.ToLower().Equals(oldCategoryName.ToLower()));
                    //if (index != -1)
                    //    this.products[index].Category = newCategoryName;
                    p.Category = newCategoryName;
                    await _products.ReplaceOneAsync(x => x.Id == p.Id, p);
                }
            }

            return true;
        }

        public async Task<bool> DeleteProductById (int productId)
        {
            Product productExists = await GetProduct(productId);

            if (productExists == null)
                return false;
            else
            {
                //this.products = products.Where(x => !(x.Id == productId) ).ToList();
                DeleteResult result = await _products.DeleteOneAsync(x => x.Id == productId);
                return true;
            }
           
        }

        public async Task<bool> DeleteProductsByCategory(string categoryName)
        {
            List<Product> categoryProducts = await SearchCategoryProducts(categoryName);

            if (categoryProducts.Count == 0)
                return true;
            else
            {
                foreach (Product p in categoryProducts)
                {
                    System.Diagnostics.Debug.WriteLine("Deleting product: " + p.Name);
                    //delete product
                    await DeleteProductById(p.Id);
                }
                return true;
            }
        }
    }
}

