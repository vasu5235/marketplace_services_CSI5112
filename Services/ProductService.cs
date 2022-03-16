using System;
using marketplace_services_CSI5112.Models;

namespace marketplace_services_CSI5112.Services
{
    public class ProductService
    {
        private readonly List<Product> products = new()
        {
            new Product(1, "Test-1","Sample description1", 100.0, "images/recent_images/01.jpeg","Electronics",0),
            new Product(2, "Test-2", "Sample description2", 200.0, "images/recent_images/02.jpeg", "Electronics",0),
            new Product(3, "Test-3", "Sample description3", 300.0, "images/recent_images/03.jpeg", "Electronics",0),
            new Product(4, "Test-4", "Sample description4", 400.0, "images/recent_images/04.jpeg", "Electronics",0),
            new Product(5, "Test-5", "Sample description5", 500.0, "images/recent_images/05.jpeg", "Food",0),
            new Product(6, "Test-6", "Sample description6", 600.0, "images/recent_images/06.jpeg", "Electronics",0),
        };

        public ProductService()
        {
        }

        public List<Product> GetProducts()
        {
            return this.products;
        }

        public async Task<Product> GetProduct(int Id)
        {
            return products.Find(x => x.Id.Equals(Id));
        }

        public async Task<List<Product>> SearchProducts(String productName)
        {
            return products.FindAll(p => p.Name.ToLower().Contains(productName));
        }

        public async Task<List<Product>> SearchCategoryProducts(String categoryName)
        {
            return products.FindAll(p => p.Category.ToLower().Contains(categoryName));
        }


        // returns false if product exists with newProduct.Id
        public async Task<bool> AddProduct(Product newProduct)
        {
            Product existingProduct = products.Find(x => x.Id == newProduct.Id);
            
            if (existingProduct == null)
            {
                products.Add(newProduct);
                return true;
            }

            return false;

        }
    }
}

