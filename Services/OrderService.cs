using System;
using marketplace_services_CSI5112.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace marketplace_services_CSI5112.Services
{
    public class OrderService
    {
        
        //private readonly Dictionary<String, List<Product>> orders = new()
        //{
        //    {
        //        "111-222",
        //        new List<Product>() {
        //            new Product("", 1, "iPhone 123","Sample description1", 100.0, "images/product_images/iphone.jpg","Electronics",1),
        //            new Product("", 2, "iPhone 3", "Sample description2", 200.0, "images/product_images/iphone.jpg", "Electronics",1),
        //            new Product("", 3, "iPhone 10", "Sample description3", 300.0, "images/product_images/iphone.jpg", "Electronics",1),
        //    }
        //    },
        //    {
        //        "111-333",
        //        new List<Product>() {
        //            new Product("", 1, "iPhone 456","Sample description1", 190.0, "images/product_images/iphone.jpg","Electronics",1),
        //            new Product("", 2, "iPhone 30", "Sample description2", 280.0, "images/product_images/iphone.jpg", "Electronics",1),
        //            new Product("", 3, "iPhone 7", "Sample description3", 360.0, "images/product_images/iphone.jpg", "Electronics",1),
        //        }
        //    },

        //    {
        //        "444-555",
        //        new List<Product>() {
        //            new Product("", 1, "iPhone 345","Sample description1", 109.0, "images/product_images/iphone.jpg","Electronics",1),
        //            new Product("", 2, "iPhone 9", "Sample description2", 208.0, "images/product_images/iphone.jpg", "Electronics",1),
        //            new Product("", 3, "iPhone 5", "Sample description3", 307.0, "images/product_images/iphone.jpg", "Electronics",1),
        //        }
        //    }
        //};
        private readonly IMongoCollection<Orders> _orders;
        private readonly string CONNECTION_STRING;

        public OrderService(IOptions<DatabaseSettings> DatabaseSettings, IConfiguration configuration)
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
            _orders = database.GetCollection<Orders>("Orders");
        }

        public async Task<bool> OrderExists(String id)
        {
            Orders existingOrder = await _orders.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();

            return existingOrder != null;
        }

        

        public async Task<Dictionary<String, List<Product>>> GetOrders()
        {
            //return this.orders;

            List<Orders> allOrderList = await _orders.Find(_ => true).ToListAsync();
            Dictionary<String, List<Product>> allOrders = new();
            
            foreach (Orders o in allOrderList)
            {
                allOrders.Add(o.Id, o.ProductList);
            }

            return allOrders;
        }

        public async Task<Dictionary<String, List<Product>>> GetOrdersByUserId(String UserId)
        {
            Dictionary<String, List<Product>> FilteredOrders = new();

            List<Orders> allOrderList = await _orders.Find(_ => true).ToListAsync();
            foreach (Orders currentOrder in allOrderList)
            {
                //order dictionary key is of format: userId-orderId
                String _UserId = currentOrder.Id.Split('-')[0];

                if (_UserId.Equals(UserId))
                {
                    FilteredOrders.Add(currentOrder.Id, currentOrder.ProductList);
                }
            }

            return FilteredOrders;
        }

        public async Task<List<Product>> GetOrdersByOrderId(String OrderId)
        {
            List<Product> FilteredOrders = new();
            List<Orders> allOrderList = await _orders.Find(_ => true).ToListAsync();

            foreach (Orders currentOrder in allOrderList)
            {
                //order dictionary key is of format: userId-orderId
                if (currentOrder.Id.Split('-').Length != 2)
                    continue;

                String _OrderId = currentOrder.Id.Split('-')[1];

                if (_OrderId.Equals(OrderId))
                {
                    FilteredOrders = currentOrder.ProductList;
                }
            }

            return FilteredOrders;
        }


        // returns false if category exists with newCategory.Id
        public async Task<bool> AddOrder(String id, List<Product> order)
        {
            Console.Write("---debug--- order.Id = " + id);
            Orders existingOrder = await _orders.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            //id should be in format: userid-orderid, validation is done in the corresponding controller class
            if (existingOrder == null)
            {
                Orders newOrder = new Orders("", id, order);
                await _orders.InsertOneAsync(newOrder);
                return true;
            }

            return false;

        }
    }
}

