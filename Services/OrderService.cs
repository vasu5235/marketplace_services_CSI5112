using System;
using marketplace_services_CSI5112.Models;

namespace marketplace_services_CSI5112.Services
{
    public class OrderService
    {
        private readonly Dictionary<int, List<Product>> orders = new()
        {
            {
                1, new List<Product> () {
                    new Product(1, "iPhone 123","Sample description1", 100.0, "images/product_images/iphone.jpg","Electronics"),
                    new Product(2, "iPhone 3", "Sample description2", 200.0, "images/product_images/iphone.jpg", "Electronics"),
                    new Product(3, "iPhone 10", "Sample description3", 300.0, "images/product_images/iphone.jpg", "Electronics"),
                }
            },
            {
                2,
                new List<Product>() {
                    new Product(1, "iPhone 456","Sample description1", 190.0, "images/product_images/iphone.jpg","Electronics"),
                    new Product(2, "iPhone 30", "Sample description2", 280.0, "images/product_images/iphone.jpg", "Electronics"),
                    new Product(3, "iPhone 7", "Sample description3", 360.0, "images/product_images/iphone.jpg", "Electronics"),
                }
            },
            {
                3,
                new List<Product>() {
                    new Product(1, "iPhone 345","Sample description1", 109.0, "images/product_images/iphone.jpg","Electronics"),
                    new Product(2, "iPhone 9", "Sample description2", 208.0, "images/product_images/iphone.jpg", "Electronics"),
                    new Product(3, "iPhone 5", "Sample description3", 307.0, "images/product_images/iphone.jpg", "Electronics"),
                }
            }
        };

        public async Task<bool> OrderExists(int id)
        {
            return orders.ContainsKey(id);
        }

        public OrderService()
        {
        }

        public Dictionary<int,List<Product>> GetOrders()
        {
            return this.orders;
        }

        public async Task<List<Product>> GetOrder(int Id)
        {
            return orders[Id];
        }


        // returns false if category exists with newCategory.Id
        public async Task<bool> AddOrder(int id, List<Product> order)
        {
            Console.Write("---debug--- order.Id = " + id);
            if (!orders.ContainsKey(id))
            {
                orders.Add(id, order);
                return true;
            }

            return false;

        }
    }
}

