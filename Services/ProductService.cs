using System;
using marketplace_services_CSI5112.Models;

namespace marketplace_services_CSI5112.Services
{
    public class ProductService
    {
        private List<Product> products = new()
        {
            new Product(1, "Test-1","Sample description1", 100.0, "images/recent_images/01.jpeg","Electronics",1),
            new Product(2, "Test-2", "Sample description2", 200.0, "images/recent_images/02.jpeg", "Electronics",1),
            new Product(3, "Test-3", "Sample description3", 300.0, "images/recent_images/03.jpeg", "Electronics",1),
            new Product(4, "Test-4", "Sample description4", 400.0, "images/recent_images/04.jpeg", "Electronics",1),
            new Product(5, "Test-5", "Sample description5", 500.0, "images/recent_images/05.jpeg", "Food",1),
            new Product(6, "Test-6", "Sample description6", 600.0, "images/recent_images/06.jpeg", "Electronics",1),
        };

        public ProductService()
        {
        }

        public async Task<List<Product>> GetProducts()
        {
            return this.products;
        }

        public async Task<Product> GetProduct(int Id)
        {
            return products.Find(x => x.Id == Id);
        }

        public async Task<List<Product>> SearchProducts(String productName)
        {
            return products.FindAll(p => p.Name.ToLower().Contains(productName.ToLower()));
        }

        public async Task<List<Product>> SearchCategoryProducts(String categoryName)
        {
            return products.FindAll(p => p.Category.ToLower().Contains(categoryName.ToLower()));
        }


        // returns false if product exists with newProduct.Id
        public async Task<bool> AddProduct(Product newProduct)
        {
            Product existingProduct = products.Find(x => x.Id == newProduct.Id || x.Name.ToLower().Equals(newProduct.Name.ToLower()) );
            
            if (existingProduct == null)
            {
                products.Add(newProduct);
                return true;
            }

            return false;

        }

        public async Task<bool> EditProduct(Product editedProduct)
        {
            int existingProductIndex = products.FindIndex(x => x.Id == editedProduct.Id);

            if (existingProductIndex == -1)
                return false;
            else
            {
                this.products[existingProductIndex] = editedProduct;
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
                    System.Diagnostics.Debug.WriteLine("Editing categoryname for product: " + p.Name);
                    int index = this.products.FindIndex(x => x.Category.ToLower().Equals(oldCategoryName.ToLower()));
                    if (index != -1)
                        this.products[index].Category = newCategoryName;
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
                this.products = products.Where(x => !(x.Id == productId) ).ToList();
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

