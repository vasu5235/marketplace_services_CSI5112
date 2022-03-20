using System;
using marketplace_services_CSI5112.Models;

namespace marketplace_services_CSI5112.Services
{
    public class OrderService
    {
        
        private readonly Dictionary<String, List<Product>> orders = new()
        {
            {
                "111-222",
                new List<Product>() {
                    new Product(1, "iPhone 123","Sample description1", 100.0, "images/product_images/iphone.jpg","Electronics",1),
                    new Product(2, "iPhone 3", "Sample description2", 200.0, "images/product_images/iphone.jpg", "Electronics",1),
                    new Product(3, "iPhone 10", "Sample description3", 300.0, "images/product_images/iphone.jpg", "Electronics",1),
            }
            },
            {
                "111-333",
                new List<Product>() {
                    new Product(1, "iPhone 456","Sample description1", 190.0, "images/product_images/iphone.jpg","Electronics",1),
                    new Product(2, "iPhone 30", "Sample description2", 280.0, "images/product_images/iphone.jpg", "Electronics",1),
                    new Product(3, "iPhone 7", "Sample description3", 360.0, "images/product_images/iphone.jpg", "Electronics",1),
                }
            },

            {
                "444-555",
                new List<Product>() {
                    new Product(1, "iPhone 345","Sample description1", 109.0, "images/product_images/iphone.jpg","Electronics",1),
                    new Product(2, "iPhone 9", "Sample description2", 208.0, "images/product_images/iphone.jpg", "Electronics",1),
                    new Product(3, "iPhone 5", "Sample description3", 307.0, "images/product_images/iphone.jpg", "Electronics",1),
                }
            }
        };

        public async Task<bool> OrderExists(String id)
        {
            return orders.ContainsKey(id);
        }

        public OrderService()
        {
        }

        public async Task<Dictionary<String, List<Product>>> GetOrders()
        {
            return this.orders;
        }

        public async Task<Dictionary<String, List<Product>>> GetOrdersByUserId(String UserId)
        {
            Dictionary<String, List<Product>> FilteredOrders = new();

            foreach (KeyValuePair<String, List<Product>> currentOrder in this.orders)
            {
                //order dictionary key is of format: userId-orderId
                String _UserId = currentOrder.Key.Split('-')[0];

                if (_UserId.Equals(UserId))
                {
                    FilteredOrders.Add(currentOrder.Key, currentOrder.Value);
                }
            }

            return FilteredOrders;
        }

        public async Task<List<Product>> GetOrdersByOrderId(String OrderId)
        {
            List<Product> FilteredOrders = new();
            foreach (KeyValuePair<String, List<Product>> currentOrder in this.orders)
            {
                //order dictionary key is of format: userId-orderId
                if (currentOrder.Key.Split('-').Length != 2)
                    continue;

                String _OrderId = currentOrder.Key.Split('-')[1];

                if (_OrderId.Equals(OrderId))
                {
                    FilteredOrders = currentOrder.Value;
                }
            }

            return FilteredOrders;
        }


        // returns false if category exists with newCategory.Id
        public async Task<bool> AddOrder(String id, List<Product> order)
        {
            Console.Write("---debug--- order.Id = " + id);
            //id should be in format: userid-orderid, validation is done in the corresponding controller class
            if (!orders.ContainsKey(id))
            {
                orders.Add(id, order);
                return true;
            }

            return false;

        }
    }
}

