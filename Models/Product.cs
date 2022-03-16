using System;
namespace marketplace_services_CSI5112.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public Product(int Id, string Name, string Description, double Price, string ImageUrl, string Category, int Quantity)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Price = Price;
            this.ImageUrl = ImageUrl;
            this.Category = Category;
            this.Quantity = Quantity;
        }
    }
}

